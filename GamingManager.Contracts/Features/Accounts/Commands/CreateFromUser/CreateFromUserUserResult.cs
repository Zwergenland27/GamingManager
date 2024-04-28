using GamingManager.Contracts.Features.Users;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Accounts.Commands.CreateFromUser;

public class CreateFromUserUserResult(
	string Id,
	string Username,
	bool Verified) : UserCoreResult(Id, Username)
{
	/// <summary>
	/// True, if the account was online at least once
	/// </summary>
	[Required]
	public bool Verified { get; init; } = Verified;
}
