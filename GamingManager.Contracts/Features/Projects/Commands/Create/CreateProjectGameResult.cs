using GamingManager.Contracts.Features.Games;

namespace GamingManager.Contracts.Features.Projects.Commands.Create;

public class CreateProjectGameResult(
	string Id,
	string Name) : GameCoreResult(Id, Name)
{
}
