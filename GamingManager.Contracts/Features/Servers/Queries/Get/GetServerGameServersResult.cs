using GamingManager.Contracts.Features.GameServers;

namespace GamingManager.Contracts.Features.Servers.Queries.Get;

public class GetServerGameServersResult(
	string Id,
	string Name,
	string Status) : GameServerCoreResult(Id, Name, Status)
{
}
