using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.Entities;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record ParticipantBannedEvent(ProjectId Project, ParticipantId Participant, Ban Details) : IDomainEvent;
