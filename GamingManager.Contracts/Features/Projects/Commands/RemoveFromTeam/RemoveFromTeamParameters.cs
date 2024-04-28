using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands.RemoveFromTeam;

/// <summary>
/// Parameters for removing a player from a team
/// </summary>
public class RemoveFromTeamParameters : IParameters
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
}
