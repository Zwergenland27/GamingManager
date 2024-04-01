using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Domain.GameServers.Events;
public record GameServerCreatedEvent(GameServerId GameServer) : IDomainEvent;
