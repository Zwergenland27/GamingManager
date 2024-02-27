using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record ProjectStartRescheduledEvent(ProjectId Project, ProjectStartsAtUtc Old, ProjectStartsAtUtc New) : IDomainEvent;
