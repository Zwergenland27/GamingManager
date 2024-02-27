using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.Servers.Events;

public record ServerStatusChangedEvent(ServerId Server) : IDomainEvent;
