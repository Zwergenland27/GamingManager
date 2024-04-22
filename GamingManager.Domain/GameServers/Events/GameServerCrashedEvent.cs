using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.GameServers.Events;

public record GameServerCrashedEvent (GameServerId GameServer, ProjectId Project, GameServerCrashedAtUtc CrashedAtUtc) : IDomainEvent;
