using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Servers.Events;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.Servers;

/// <summary>
/// A hardware server on which many game servers can run
/// </summary>
public class Server : AggregateRoot<ServerId>
{
	private Server(
		Hostname hostname,
		Uri address,
		Mac mac,
		ServerAutoShutdownDelay shutdownDelay) : base(ServerId.CreateNew())
	{
		Hostname = hostname;
		Address = address;
		Mac = mac;
		Status = ServerStatus.Offline;
		ShutdownDelay = shutdownDelay;
		Maintenance = true;
		PossiblyUnstartable = false;
	}

#pragma warning disable CS8618
	private Server() : base(default!) { }
#pragma warning restore CS8618

	public Hostname Hostname { get; private set; }

	public Uri Address { get; set; }

	public Mac Mac { get; private init; }

	public ServerStatus Status { get; private set; }

	public ServerAutoShutdownDelay ShutdownDelay { get; set; }

	public ServerShutdownAtUtc? ShutdownAt { get; private set; }

	public HeartbeatReceivedAtUtc? LastHeartbeatAt { get; private set; }

	public bool Maintenance { get; private set; }

	public bool PossiblyUnstartable { get; private set; }

	public static CanFail<Server> Create(Hostname hostname, Uri address, Mac mac, ServerAutoShutdownDelay shutdownDelay)
	{
		return new Server(hostname, address, mac, shutdownDelay);
	}

	public void ChangeHostname(Hostname hostname)
	{
		Hostname = hostname;
	}

	public void ChangeAddress(Uri address)
	{
		Address = address;
		RaiseDomainEvent(new ServerAddressChangedEvent(Id, address));
	}

	public CanFail Start()
	{
		if (Maintenance) return Errors.Servers.NotStartable;
		if (Status == ServerStatus.Online) return Errors.Servers.AlreadyOnline;
		if (Status == ServerStatus.Starting) return Errors.Servers.AlreadyStarting;

		Status = ServerStatus.Starting;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id, Status));
		return CanFail.Success();
	}

	public void StartFailed()
	{
		Status = ServerStatus.Offline;
		PossiblyUnstartable = true;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id, Status));
	}

	public CanFail Crashed()
	{
		if (Status == ServerStatus.Offline) return Errors.GameServers.CannotCrash;

		Status = ServerStatus.Offline;
		ShutdownAt = null;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id, Status));

		ServerCrashedAtUtc crashedAt = new ServerCrashedAtUtc(DateTime.UtcNow);
		if (LastHeartbeatAt is not null)
		{
			crashedAt = new ServerCrashedAtUtc(LastHeartbeatAt.Value);
		}

		RaiseDomainEvent(new ServerCrashedEvent(Id, crashedAt));
		return CanFail.Success();
	}

	public CanFail ScheduleShutdown(ServerShutdownAtUtc shutdownAtUtc)
	{
		if (shutdownAtUtc.Value < DateTime.UtcNow) return Errors.Servers.ShutdownInPast;

		ShutdownAt = shutdownAtUtc;
		RaiseDomainEvent(new ServerShutdownScheduledEvent(Id));
		return CanFail.Success();
	}

	public CanFail CancelShutdown()
	{
		if (ShutdownAt is null) return Errors.Servers.NoShutdownScheduled;
		ShutdownAt = null;
		RaiseDomainEvent(new ServerShutdownCancelledEvent(Id));
		return CanFail.Success();
	}

	public CanFail Shutdown()
	{
		if (Status == ServerStatus.Starting) return Errors.Servers.CannotShutdownStarting;
		if (Status == ServerStatus.Offline) return Errors.Servers.CannotShutdownOffline;
		if (ShutdownAt is null) return Errors.Servers.NoShutdownScheduled;
		if (ShutdownAt.Value < DateTime.UtcNow) return Errors.Servers.ShutdownTooEarly;

		Status = ServerStatus.Offline;
		ShutdownAt = null;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id, Status));
		return CanFail.Success();
	}

	public CanFail BeginMaintenance()
	{
		throw new NotImplementedException("Not working yet.");
		Maintenance = true;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id, Status));
		return CanFail.Success();
	}

	public CanFail FinishMaintenance()
	{
		throw new NotImplementedException("Not working yet.");
		if (!Maintenance) return Errors.Servers.NoMaintenance;
		Maintenance = false;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id, Status));
		return CanFail.Success();
	}

	public CanFail Heartbeat(HeartbeatReceivedAtUtc utcTime)
	{
		if (LastHeartbeatAt is not null && LastHeartbeatAt.Value >= utcTime.Value) return Errors.Servers.PingBeforeLast;

		LastHeartbeatAt = utcTime;

		if (Status == ServerStatus.Starting || Status == ServerStatus.Offline)
		{
			PossiblyUnstartable = false;
			Status = ServerStatus.Online;
			RaiseDomainEvent(new ServerStatusChangedEvent(Id, Status));
		}

		return CanFail.Success();
	}
}
