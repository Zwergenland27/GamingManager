using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Domain.GameServers.Events;
public record GameServerShutdownStartedEvent(GameServerId GameServer) : IDomainEvent;
