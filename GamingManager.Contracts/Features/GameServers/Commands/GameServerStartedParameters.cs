using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServers.Commands;

/// <summary>
/// Parameters when a gameserver is started
/// </summary>
public class GameServerStartedParameters : IParameters
{
	///<summary>
	/// Name of the gameserver
	/// </summary>
	/// <example>Minecraft-04</example>
	[JsonIgnore]
	public string? GameServerName { get; set; }
}
