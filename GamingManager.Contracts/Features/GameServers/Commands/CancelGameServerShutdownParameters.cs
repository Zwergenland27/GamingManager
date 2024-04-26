using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServers.Commands;

/// <summary>
/// Parameters for the CancelShutdown command
/// </summary>
public record CancelGameServerShutdownParameters : IParameters
{
	///<summary>
	/// Name of the gameserver
	/// </summary>
	/// <example>Minecraft-04</example>
	[JsonIgnore]
	public string? GameServerName { get; set; }
}
