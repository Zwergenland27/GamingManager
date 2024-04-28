namespace GamingManager.Contracts.Features.Games.Queries.Get;

public class GetGameAccountsResult(
	string Id,
	string Name) : GameCoreResult(Id, Name)
{
}
