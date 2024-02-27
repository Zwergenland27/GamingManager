using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Domain.GameServers.Events;

public record GameServerCrashedEvent (GameServerId GameServer, CrashedAtUtc CrashedAt) : IDomainEvent;
