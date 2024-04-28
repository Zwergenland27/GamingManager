using GamingManager.Contracts.Features.Accounts;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Users.Queries.Get;

public class GetUserAccountResult(
	string Id,
	string Name,
	GetUserAccountGameResult Game) : AccountCoreResult(Id, Name)
{
	/// <summary>
	/// The game of the account
	/// </summary>
	[Required]
	public GetUserAccountGameResult Game { get; init; } = Game;
}
