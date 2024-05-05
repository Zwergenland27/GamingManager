using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record MemberAddedEvent(ProjectId Project, UserId User, MemberId TeamMember, MemberRole Role) : IDomainEvent;
