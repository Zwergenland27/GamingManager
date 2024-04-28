using GamingManager.Contracts.Features.GameServers;

namespace GamingManager.Contracts.Features.Projects.Queries.Get;

public class GetProjectGameServerResult(
	string Id,
	string Name,
	string Status) : GameServerCoreResult(Id, Name, Status)
{
}
