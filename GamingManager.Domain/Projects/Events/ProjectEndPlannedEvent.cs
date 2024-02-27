using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record ProjectEndPlannedEvent(ProjectId Project, ProjectEndsAtUtc EndsAt) : IDomainEvent;
