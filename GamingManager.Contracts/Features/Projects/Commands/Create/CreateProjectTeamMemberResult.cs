using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.Commands.Create;

public class CreateProjectTeamMemberResult(
	string Id,
	string Role,
	DateTime SinceUtc,
	CreateTeamMemberUserResult User) : TeamMemberCoreResult(Id, Role, SinceUtc)
{
	/// <summary>
	/// User information of the member
	/// </summary>
	[Required]
	public CreateTeamMemberUserResult User { get; init; } = User;
}
