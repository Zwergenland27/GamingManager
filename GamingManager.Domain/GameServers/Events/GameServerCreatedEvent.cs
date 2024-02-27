using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.ProjectServers;

namespace GamingManager.Domain.GameServers.Events;
public record GameServerCreatedEvent(GameServerId GameServer) : IDomainEvent;
