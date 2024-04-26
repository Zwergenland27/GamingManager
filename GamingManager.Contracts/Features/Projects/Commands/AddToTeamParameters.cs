using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands;

/// <summary>
/// Parameters for adding a user to a project
/// </summary>
public class AddToTeamParameters : IParameters
{
	///<summary>
	/// Unique id of the project
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[JsonIgnore]
	public string? ProjectId { get; set; }

	/// <summary>
	/// Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[Required]
	public string? Username { get; set; }

	/// <summary>
	/// The role of the member
	/// </summary>
	/// <example>Admin</example>
	[Required]
	public string? Role { get; set; }
}
