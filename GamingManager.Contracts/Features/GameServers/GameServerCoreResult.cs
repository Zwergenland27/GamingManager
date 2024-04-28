using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServers;

public class GameServerCoreResult(
	string Id,
	string Name,
	string Status)
{
	/// <summary>
	/// Unique id of the server
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	/// <summary>
	/// Name of the gameserver
	/// </summary>
	/// <example>Minecraft-04</example>
	[Required]
	public string Name { get; init; } = Name;

	/// <summary>
	/// Status of the server
	/// </summary>
	/// 
	[Required]
	public string Status { get; init; } = Status;
}
