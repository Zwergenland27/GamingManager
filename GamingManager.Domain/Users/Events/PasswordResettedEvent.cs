using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Users.Events;

public record PasswordResettedEvent(Username Username, Email Email, Password NewPassword) : IDomainEvent;
