using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Domain.GameServers.Events;

public record GameServerStatusChangedEvent(GameServerId ProjectServer, GameServerStatus Status) : IDomainEvent;
