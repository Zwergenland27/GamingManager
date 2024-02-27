using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record TeamMemberAddedEvent(ProjectId Project, UserId User, TeamMemberId TeamMember, TeamRole Role) : IDomainEvent;
