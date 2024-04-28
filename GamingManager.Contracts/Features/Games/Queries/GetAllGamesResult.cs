namespace GamingManager.Contracts.Features.Games.Queries;

public class GetAllGamesResult(
	string Id,
	string Name) : GameCoreResult(Id, Name)
{
}
