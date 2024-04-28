using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Users.Events;

public record EmailVerificationRequestedEvent(Username Username, Email Email, EmailVerificationToken VerificationToken) : IDomainEvent;
