using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.GameServers.Events;

public record GameServerHostServerChangedEvent(ServerId OldServer, GameServerId GameServer) : IDomainEvent;
