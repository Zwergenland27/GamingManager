using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServers.Commands.Start;

/// <summary>
/// Parameters for starting a gameserver
/// </summary>
public class StartGameServerParameters : IParameters
{
    ///<summary>
    /// Name of the gameserver
    /// </summary>
    /// <example>Minecraft-04</example>
    [Required]
    public string? Name { get; set; }
}
