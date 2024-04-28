using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServers.Commands.ChangeShutdownDelay;

/// <summary>
/// Parameters for changing the shutdown delay
/// </summary>
public class ChangeGameServerShutdownDelayParameters : IParameters
{
    /// <summary>
    /// Name of the gameserver
    /// </summary>
    /// <example>Minecraft-04</example>
    [JsonIgnore]
    public string? GameServerName { get; set; }

    /// <summary>
    /// Shutdown delay after last player left in minutes
    /// </summary>
    /// <example>15</example>
    [Required]
    public uint? Delay { get; set; }
}
