using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects;

public class TeamMemberCoreResult(
	string Id,
	string Role,
	DateTime SinceUtc)
{
	///<summary>
	/// Unique id of the member
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	/// <summary>
	/// The role of the member
	/// </summary>
	/// <example>Admin</example>
	[Required]
	public string Role { get; init; } = Role;

	/// <summary>
	/// Since when the member is part of the project team
	/// </summary>
	[Required]
	public DateTime SinceUtc { get; init; } = SinceUtc;
}
