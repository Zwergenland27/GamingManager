using GamingManager.Contracts.Features.Games;

namespace GamingManager.Contracts.Features.Accounts.Queries.Get;

public class GetAccountGameResult(
		string Id,
			string Name) : GameCoreResult(Id, Name)
{
}
