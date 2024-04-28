using GamingManager.Contracts.Features.Games;

namespace GamingManager.Contracts.Features.Projects.Queries.Get;

public class GetProjectGameResult(
	string Id,
	string Name) : GameCoreResult(Id, Name)
{
}