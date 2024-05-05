using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServers.Commands.Crashed;

/// <summary>
/// Parameters for a crashed gameserver
/// </summary>
public class GameServerCrashedParameters : IParameters
{
    /// <summary>
    /// Name of the gameserver
    /// </summary>
    /// <example>Minecraft-04</example>
    [JsonIgnore]
    public string? Name { get; set; }

    /// <summary>
    /// The time the server crashed
    /// </summary>
    [Required]
    public DateTime? CrashedAtUtc { get; set; }
}
