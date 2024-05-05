using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Games.Queries.Get;

public class GetGameResult(
	string Id,
	string Name,
	bool? VerificationRequired,
	IReadOnlyCollection<GetGameAccountsResult> Accounts,
	IReadOnlyCollection<GetGameProjectsResult> Projects) : GameCoreResult(Id, Name)
{
	/// <summary>
	/// True, if the player must authorize when creating its account
	/// </summary>
	/// <remarks>
	/// This should only be visible to admins
	/// </remarks>
	public bool? VerificationRequired { get; init; } = VerificationRequired;
	/// <summary>
	/// All Accounts of the game
	/// </summary>
	[Required]
	public IReadOnlyCollection<GetGameAccountsResult> Accounts { get; init; } = Accounts;

	/// <summary>
	/// All projects of the game
	/// </summary>
	[Required]
	public IReadOnlyCollection<GetGameProjectsResult> Projects { get; init; } = Projects;
}
