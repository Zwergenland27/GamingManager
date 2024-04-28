using GamingManager.Contracts.Features.Projects;

namespace GamingManager.Contracts.Features.Games.Queries.Get;

public class GetGameProjectsResult(
	string Id,
	string Name) : ProjectCoreResult(Id, Name)
{
}
