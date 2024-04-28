using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Accounts.Commands.AssignUuid;

public record AssignUuidParameters : IParameters
{
    ///<summary>
    /// Name of the game
    /// </summary>
    /// <example>Minecraft</example>
    [JsonIgnore]
    public string? GameName { get; init; }

    ///<summary>
    /// Name of the account
    /// </summary>
    /// <example>Zwergenland</example>
    [JsonIgnore]
    public string? AccountName { get; init; }

    ///<summary>
    /// Uuid of the account
    /// </summary>
    /// <example>MinecraftUUID</example>
    [Required]
    public string? Uuid { get; init; }
}
