using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServers.Commands.Create;

/// <summary>
/// Parameters for creating a gameserver
/// </summary>
public class CreateGameServerParameters : IParameters
{
    /// <summary>
    /// Name of the gameserver
    /// </summary>
    /// <example>Minecraft-04</example>
    [JsonIgnore]
    public string? GameServerName { get; set; }

    /// <summary>
    /// The project the server belongs to
    /// </summary>
    /// <example>Survivalcraft</example>
    [Required]
    public string? ProjectName { get; set; }

    /// <summary>
    /// Shutdown delay after last player left in minutes
    /// </summary>
    /// <example>15</example>
    [Required]
    public uint? ShutdownDelay { get; set; }
}
