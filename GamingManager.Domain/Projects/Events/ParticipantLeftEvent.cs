using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record ParticipantLeftEvent(ProjectId Project, ParticipantId Participant, SessionEndsAtUtc LeftAt, bool Irregular = false) : IDomainEvent;
