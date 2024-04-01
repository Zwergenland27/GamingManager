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
		Alias alias,
		Mac mac) : base(ServerId.CreateNew())
	{
		Alias = alias;
		Mac = mac;
		Status = ServerStatus.Offline;
		Maintenance = true;
		PossiblyUnstartable = false;
	}

#pragma warning disable CS8618
	private Server() : base(default!) { }
#pragma warning restore CS8618

	public Alias Alias { get; set; }

	public Mac Mac { get; set; }

	public ServerStatus Status { get; private set; }

	public ServerShutdownAtUtc? ShutdownAt { get; private set; }

	public HeartbeatReceivedAtUtc? LastHeartbeatAt { get; private set; }

	public bool Maintenance { get; private set; }

	public bool PossiblyUnstartable { get; private set; }

	public static CanFail<Server> Create(Alias alias, Mac mac)
	{
		return new Server(alias, mac);
	}

	public CanFail Start()
	{
		if (Maintenance) return Errors.Servers.NotStartable;
		if (Status == ServerStatus.Online) return Errors.Servers.AlreadyOnline;
		if (Status == ServerStatus.Starting) return Errors.Servers.AlreadyStarting;

		Status = ServerStatus.Starting;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id));
		return CanFail.Success();
	}

	public void StartFailed()
	{
		Status = ServerStatus.Offline;
		PossiblyUnstartable = true;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id));
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
		RaiseDomainEvent(new ServerStatusChangedEvent(Id));
		return CanFail.Success();
	}

	public CanFail BeginMaintenance()
	{
		Maintenance = true;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id));
		return CanFail.Success();
	}

	public CanFail FinishMaintenance()
	{
		if (!Maintenance) return Errors.Servers.NoMaintenance;
		Maintenance = false;
		RaiseDomainEvent(new ServerStatusChangedEvent(Id));
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
			RaiseDomainEvent(new ServerStatusChangedEvent(Id));
		}

		return CanFail.Success();
	}
}
