using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Infrastructure.ReadModels;

public class MemberReadModel
{
	public Guid Id { get; set; }

	public Guid ProjectId { get; set; }

	public ProjectReadModel Project { get; set; }

	public Guid UserId { get; set; }

	public UserReadModel User { get; set; }

	public MemberRole Role { get; set; }

	public DateTime TeamMemberSince { get; set; }
}
