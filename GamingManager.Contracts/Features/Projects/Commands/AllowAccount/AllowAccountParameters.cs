using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands.AllowAccount;

/// <summary>
/// Parameters for allowing an account to join a project.
/// </summary>
public class AllowAccountParameters : IParameters
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
}
