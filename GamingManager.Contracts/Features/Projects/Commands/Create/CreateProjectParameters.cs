using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands.Create;

/// <summary>
/// Parameters for creating a project
/// </summary>
public class CreateProjectParameters : IParameters
{

	/// <summary>
	/// Id of the user that is creating the project
	/// </summary>
	[JsonIgnore]
	public string? AuditorId { get; set; }

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
}
