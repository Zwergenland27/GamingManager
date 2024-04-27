using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Users.Events;

public record UserCreatedEvent(Username Username, Email Email) : IDomainEvent;
