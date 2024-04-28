using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServers.Commands.ShuttedDown;

/// <summary>
/// Parameters when a gameserver is shutted down
/// </summary>
public class GameServerShuttedDownParameters : IParameters
{
    ///<summary>
    /// Name of the gameserver
    /// </summary>
    /// <example>Minecraft-04</example>
    [JsonIgnore]
    public string? Name { get; set; }
}
