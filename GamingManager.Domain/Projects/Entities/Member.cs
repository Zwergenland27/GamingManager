using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Projects.Entities;

public class Member : Entity<MemberId>
{
	internal Member(
		ProjectId projectId,
		UserId userId,
		MemberRole role) : base(MemberId.CreateNew())
	{
		ProjectId = projectId;
		UserId = userId;
		Role = role;
		Since = new TeamMemberSinceUtc(DateTime.UtcNow);
	}

#pragma warning disable CS8618
	private Member() : base(default!) { }
#pragma warning restore CS8618

	public ProjectId ProjectId { get; }

	public UserId UserId { get; private init; }

	public MemberRole Role { get; set; }

	public TeamMemberSinceUtc Since { get; private init; }
}
