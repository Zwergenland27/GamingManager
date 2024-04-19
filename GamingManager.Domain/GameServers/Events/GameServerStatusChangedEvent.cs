using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.GameServers.Events;

public record GameServerStatusChangedEvent(GameServerId ProjectServer, ServerId Server, GameServerStatus Status) : IDomainEvent;
