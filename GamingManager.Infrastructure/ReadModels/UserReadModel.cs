using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Infrastructure.ReadModels;

public class UserReadModel
{
	public Guid Id { get; set; }

	public string Username { get; set; }

	public string? Firstname { get; set; }

	public string? Lastname { get; set; }

	public string Email { get; set; }

	public Role Role { get; set; }

	public bool EmailConfirmed { get; set; }

	public IReadOnlyCollection<AccountReadModel> Accounts { get; set; }

	public IReadOnlyCollection<TeamMemberReadModel> MemberOfTeam { get; set; }
}
