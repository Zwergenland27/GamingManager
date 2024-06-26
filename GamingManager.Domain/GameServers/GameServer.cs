﻿using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers.Events;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.GameServers;

public class GameServer : AggregateRoot<GameServerId>
{
	private GameServer(
		ProjectId projectId,
		GameServerName servername,
		GameServerAutoShutdownDelay shutdownDelay) : base(GameServerId.CreateNew())
	{
		ProjectId = projectId;
		ServerName = servername;
		Status = GameServerStatus.Offline;
		ShutdownDelay = shutdownDelay;
		Maintenance = true;
		Unstartable = false;
	}

#pragma warning disable CS8618
	private GameServer() : base(default!) { }
#pragma warning restore CS8618

	public ServerId? HostedOnId { get; private set; }

	public ProjectId ProjectId { get; private init; }

	public GameServerName ServerName { get; private init; }

	public GameServerStatus Status { get; private set; }

	public GameServerAutoShutdownDelay ShutdownDelay { get; set; }

	public GameServerShutdownAtUtc? ShutdownAt { get; private set; }

	public string? Address { get; private set; }

	public bool Maintenance { get; private set; }

	public bool Unstartable { get; private set; }

	public static CanFail<GameServer> Create(Project project, GameServerName serverName, GameServerAutoShutdownDelay shutDownDelay)
	{
		var gameServer = new GameServer(project.Id, serverName, shutDownDelay);
		gameServer.RaiseDomainEvent(new GameServerCreatedEvent(gameServer.Id));
		return gameServer;
	}

	public CanFail Use(Server server)
	{
		if(Status != GameServerStatus.Offline) return Errors.GameServers.OfflineNeeded;
		if(!Maintenance) return Errors.GameServers.MaintenanceNeeded;

		HostedOnId = server.Id;
		HostAddressChanged(server.Address);
		return CanFail.Success();
	}

	public void HostAddressChanged(Uri newAddress)
	{
		Address = newAddress.Host;
		RaiseDomainEvent(new AddressChangedEvent(Id, Address));
	}

	public CanFail MarkForStart()
	{
		if (HostedOnId is null) return Errors.GameServers.ServerNotHosted;

		if (Status == GameServerStatus.Online) return Errors.GameServers.AlreadyOnline;
		if (Status == GameServerStatus.Starting) return Errors.GameServers.AlreadyStarting;

		Status = GameServerStatus.WaitingForHardware;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, HostedOnId, Status));
		return CanFail.Success();
	}

	public CanFail Start()
	{
		if (HostedOnId is null) return Errors.GameServers.ServerNotHosted;

		if (Maintenance) return Errors.GameServers.NotStartable;
		if (Status == GameServerStatus.Online) return Errors.GameServers.AlreadyOnline;
		if (Status == GameServerStatus.Starting) return Errors.GameServers.AlreadyStarting;
		Status = GameServerStatus.Starting;

		var shutdownAtUtc = new GameServerShutdownAtUtc(DateTime.UtcNow.AddMinutes(ShutdownDelay.Minutes));
		ScheduleShutdown(shutdownAtUtc);

		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, HostedOnId, Status));
		return CanFail.Success();
	}

	public CanFail ScheduleShutdown(GameServerShutdownAtUtc shutdownAtUtc)
	{
		if (shutdownAtUtc.Value < DateTime.UtcNow) return Errors.GameServers.ShutdownInPast;

		ShutdownAt = shutdownAtUtc;
		RaiseDomainEvent(new GameServerShutdownScheduledEvent(Id, shutdownAtUtc));
		return CanFail.Success();
	}

	public CanFail CancelShutdown()
	{
		if (ShutdownAt is null) return Errors.GameServers.NoShutdownScheduled;
		ShutdownAt = null;
		RaiseDomainEvent(new GameServerShutdownCancelledEvent(Id));
		return CanFail.Success();
	}

	public CanFail Shutdown()
	{
		if (Status == GameServerStatus.Starting) return Errors.GameServers.CannotShutdownStarting;
		if (Status == GameServerStatus.Offline) return Errors.GameServers.CannotShutdownOffline;
		if (ShutdownAt is null) return Errors.GameServers.NoShutdownScheduled;
		if (ShutdownAt.Value < DateTime.UtcNow) return Errors.GameServers.ShutdownTooEarly;
		RaiseDomainEvent(new GameServerShutdownStartedEvent(Id));
		return CanFail.Success();
	}

	public void ShuttedDown()
	{
		Status = GameServerStatus.Offline;
		ShutdownAt = null;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, HostedOnId!, Status));
	}

	public void Started()
	{
		Status = GameServerStatus.Online;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, HostedOnId!, Status));
	}

	public CanFail Crashed(GameServerCrashedAtUtc crashedAt)
	{
		if (Status == GameServerStatus.Offline) return Errors.GameServers.CannotCrash;

		Status = GameServerStatus.Offline;
		ShutdownAt = null;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, HostedOnId!, Status));
		RaiseDomainEvent(new GameServerCrashedEvent(Id, ProjectId, crashedAt));
		return CanFail.Success();
	}

	public CanFail BeginMaintenance()
	{
		throw new NotImplementedException("Not working yet.");
		Maintenance = true;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, HostedOnId, Status));
		return CanFail.Success();
	}

	public CanFail FinishMaintenance()
	{
		throw new NotImplementedException("Not working yet.");
		if (!Maintenance) return Errors.GameServers.NoMaintenance;
		Maintenance = false;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, HostedOnId, Status));
		return CanFail.Success();
	}
}
