using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.Commands.AddToTeam;

public class AddToTeamResult(
	string Id,
	string Role,
	DateTime SinceUtc,
	AddToTeamUserResult User) : TeamMemberCoreResult(Id, Role, SinceUtc)
{

	/// <summary>
	/// The user information of the member
	/// </summary>
	[Required]
	public AddToTeamUserResult User { get; init; } = User;
}
