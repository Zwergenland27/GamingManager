using GamingManager.Contracts.Features.Games;

namespace GamingManager.Contracts.Features.Users.Queries.Get;

public class GetUserAccountGameResult(
	string Id,
	string Name) : GameCoreResult(Id, Name)
{
}
