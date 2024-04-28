using GamingManager.Contracts.Features.Projects.Commands.Create;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.Queries.Get;

public class GetProjectTeamMemberResult(
	string Id,
	string Role,
	DateTime SinceUtc,
	GetProjectTeamMemberUserResult User) : TeamMemberCoreResult(Id, Role, SinceUtc)
{
	/// <summary>
	/// User information of the member
	/// </summary>
	[Required]
	public GetProjectTeamMemberUserResult User { get; init; } = User;
}
