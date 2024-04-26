using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.Servers.Events;

public record ServerAddressChangedEvent(ServerId Server, Uri NewAddress) : IDomainEvent;
