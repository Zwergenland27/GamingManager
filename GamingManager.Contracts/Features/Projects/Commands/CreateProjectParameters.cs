using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.Commands;

/// <summary>
/// Parameters for creating a project
/// </summary>
public class CreateProjectParameters : IParameters
{
	///<summary>
	/// Name of the game
	/// </summary>
	/// <example>Minecraft</example>
	[Required]
	public string? GameName { get; set; }

	/// <summary>
	/// Name of the project
	/// </summary>
	/// <example>SurvivalCraft</example>
	[Required]
	public string? ProjectName { get; set; }

	/// <summary>
	/// The start time of the project
	/// </summary>
	[Required]
	public DateTime? StartsAtUtc { get; set; }


	/// <summary>
	/// Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[Required]
	public string? Username { get; set; }

}
