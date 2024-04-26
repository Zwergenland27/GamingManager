using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServers.Commands;

public class DeleteGameServerParameters : IParameters
{
	/// <summary>
	/// Name of the gameserver
	/// </summary>
	/// <example>Minecraft-04</example>
	[JsonIgnore]
	public string? GameServerName { get; set; }
}
