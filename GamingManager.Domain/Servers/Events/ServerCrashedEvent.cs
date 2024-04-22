using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.Servers.Events;

public record ServerCrashedEvent(ServerId Server, ServerCrashedAtUtc CrashedAt) : IDomainEvent;
