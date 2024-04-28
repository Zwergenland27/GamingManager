using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Accounts.Commands.CreateFromUser;

public class CreateFromUserResult(
	string Id,
	string Name,
	string? Uuid,
	CreateFromUserUserResult? User,
	CreateFromUserGameResult Game) : AccountCoreResult(Id, Name)
{
	///<summary>
	/// Uuid of the account
	/// </summary>
	/// <example>MinecraftUUID</example>
	public string? Uuid { get; init; } = Uuid;

	/// <summary>
	/// The user that owns the account
	/// </summary>
	public CreateFromUserUserResult? User { get; init; } = User;

	/// <summary>
	/// The name of the game that the account belongs to
	/// </summary>
	[Required]
	public CreateFromUserGameResult Game { get; init; } = Game;
}