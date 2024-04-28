using GamingManager.Contracts.Features.Projects;

namespace GamingManager.Contracts.Features.GameServers.Queries.Get;

public class GetGameServerProjectResult(
	string Id,
	string Name) : ProjectCoreResult(Id, Name)
{
}
