using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers.Events;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.GameServers;

public class GameServer : AggregateRoot<GameServerId>
{
	private GameServer(
		ProjectId projectId,
		ServerName servername,
		GameServerAutoShutdownDelay shutdownDelay) : base(GameServerId.CreateNew())
	{
		Project = projectId;
		ServerName = servername;
		Status = GameServerStatus.Offline;
		ShutdownDelay = shutdownDelay;
		Maintenance = true;
		Unstartable = false;
	}

#pragma warning disable CS8618
	private GameServer() : base(default!) { }
#pragma warning restore CS8618

	public ServerId? HostedOn { get; private set; }

	public ProjectId Project { get; private init; }

	public ServerName ServerName { get; private init; }

	public GameServerStatus Status { get; private set; }

	public GameServerAutoShutdownDelay ShutdownDelay { get; set; }

	public GameServerShutdownAtUtc? ShutdownAt { get; private set; }

	public bool Maintenance { get; private set; }

	public bool Unstartable { get; private set; }

	public static CanFail<GameServer> Create(Project project, ServerName serverName, GameServerAutoShutdownDelay shutDownDelay)
	{
		var gameServer = new GameServer(project.Id, serverName, shutDownDelay);
		gameServer.RaiseDomainEvent(new GameServerCreatedEvent(gameServer.Id));
		return gameServer;
	}

	public CanFail Start()
	{
		if (Maintenance) return Errors.GameServers.NotStartable;
		if (Status == GameServerStatus.Online) return Errors.GameServers.AlreadyOnline;
		if (Status == GameServerStatus.Starting) return Errors.GameServers.AlreadyStarting;

		Status = GameServerStatus.Starting;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, Status));
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
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, Status));
	}

	public CanFail Started(StartedAtUtc startedAt)
	{
		if (startedAt.Value >= DateTime.UtcNow) return Errors.GameServers.StartedInFuture;
		Status = GameServerStatus.Online;

		return CanFail.Success();
	}

	public CanFail Crashed(CrashedAtUtc crashedAt)
	{
		if (Status == GameServerStatus.Offline) return Errors.GameServers.CannotCrash;

		Status = GameServerStatus.Offline;
		ShutdownAt = null;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, Status));
		RaiseDomainEvent(new GameServerCrashedEvent(Id, crashedAt));
		return CanFail.Success();
	}

	public CanFail BeginMaintenance()
	{
		Maintenance = true;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, Status));
		return CanFail.Success();
	}

	public CanFail FinishMaintenance()
	{
		if (!Maintenance) return Errors.GameServers.NoMaintenance;
		Maintenance = false;
		RaiseDomainEvent(new GameServerStatusChangedEvent(Id, Status));
		return CanFail.Success();
	}
}
