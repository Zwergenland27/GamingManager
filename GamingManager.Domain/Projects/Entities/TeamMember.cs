using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Projects.Entities;

public class TeamMember : Entity<TeamMemberId>
{
	internal TeamMember(
		ProjectId projectId,
		UserId userId,
		TeamRole role) : base(TeamMemberId.CreateNew())
	{
		Project = projectId;
		User = userId;
		Role = role;
		Since = new TeamMemberSinceUtc(DateTime.UtcNow);
	}

#pragma warning disable CS8618
	private TeamMember() : base(default!) { }
#pragma warning restore CS8618

	public ProjectId Project { get; }

	public UserId User { get; private init; }

	public TeamRole Role { get; set; }

	public TeamMemberSinceUtc Since { get; private init; }
}
