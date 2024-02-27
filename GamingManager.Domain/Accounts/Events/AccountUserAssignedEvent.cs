using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Accounts.Events;

public record AccountUserAssignedEvent(AccountId Player, UserId User) : IDomainEvent;
