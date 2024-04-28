using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Games;

public class GameCoreResult(
	string Id,
	string Name)
{
	///<summary>
	/// Unique id of the game
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	///<summary>
	/// Name of the game
	/// </summary>
	/// <example>Minecraft</example>
	[Required]
	public string Name { get; init; } = Name;
}
