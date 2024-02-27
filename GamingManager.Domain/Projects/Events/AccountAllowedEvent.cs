using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record AccountAllowedEvent(ProjectId Project, AccountId Account, ParticipantId Participant) : IDomainEvent;
