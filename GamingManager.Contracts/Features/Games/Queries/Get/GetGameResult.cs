namespace GamingManager.Contracts.Features.Games.Queries.Get;

public class GetGameResult(
	string Id,
	string Name,
	IReadOnlyCollection<GetGameAccountsResult> Accounts,
	IReadOnlyCollection<GetGameProjectsResult> Projects) : GameCoreResult(Id, Name)
{
	/// <summary>
	/// All Accounts of the game
	/// </summary>
	public IReadOnlyCollection<GetGameAccountsResult> Accounts { get; init; } = Accounts;

	/// <summary>
	/// All projects of the game
	/// </summary>
	public IReadOnlyCollection<GetGameProjectsResult> Projects { get; init; } = Projects;
}
