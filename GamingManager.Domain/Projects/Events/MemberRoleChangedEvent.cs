using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record MemberRoleChangedEvent(ProjectId ProjectId, MemberId MemberId, MemberRole Role) : IDomainEvent;
