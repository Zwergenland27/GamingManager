using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServers.Queries.Get;

/// <summary>
/// Parameters for getting a gameserver
/// </summary>
public class GetGameServerParameters : IParameters
{
    ///<summary>
    /// Name of the gameserver
    /// </summary>
    /// <example>Minecraft-04</example>
    [JsonIgnore]
    public string? Name { get; set; }
}
