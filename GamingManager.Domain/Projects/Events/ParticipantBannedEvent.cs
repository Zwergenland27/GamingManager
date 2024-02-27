using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record ParticipantBannedEvent(ProjectId Project, ParticipantId Participant, Reason Reason) : IDomainEvent;
