namespace GamingManager.Contracts.Features.Games.Queries.GetAll;

public class GetAllGamesResult(
    string Id,
    string Name) : GameCoreResult(Id, Name)
{
}
