using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands.Ban;

/// <summary>
/// parameters for banning an account
/// </summary>
public class BanAccountParameters : IParameters
{
    ///<summary>
    /// Unique id of the project
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [JsonIgnore]
    public string? ProjectId { get; set; }

    ///<summary>
    /// Unique id of the account
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [Required]
    public string? AccountId { get; set; }

    /// <summary>
    /// Reason of the ban
    /// </summary>
    /// <example>Griefing</example>
    [Required]
    public string? Reason { get; set; }

    /// <summary>
    /// Duration of the ban
    /// </summary>
    /// <remarks>
    /// If this field is null, the ban is permanent
    /// </remarks>
    public TimeSpan Duration { get; set; }
}
