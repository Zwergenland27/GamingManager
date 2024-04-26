using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands;

/// <summary>
/// Parameters for joining a project
/// </summary>
public class JoinParameters : IParameters
{
	///<summary>
	/// Unique id of the project
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[JsonIgnore]
	public string? ProjectId { get; set; }

	///<summary>
	/// Uuid of the account
	/// </summary>
	/// <example>MinecraftUUID</example>
	[Required]
	public string? Uuid { get; set; }

	/// <summary>
	/// Time when the player joined the game
	/// </summary>
	[Required]
	public DateTime? JoinTimeUtc { get; set; }
}
