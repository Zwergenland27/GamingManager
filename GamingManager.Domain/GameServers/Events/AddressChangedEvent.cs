using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Domain.GameServers.Events;

public record AddressChangedEvent(GameServerId GameServerId, string Address) : IDomainEvent;
