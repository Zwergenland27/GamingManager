using GamingManager.Contracts.Features.Servers;

namespace GamingManager.Contracts.Features.GameServers.Commands.UseServer;

public class UseServerResult(
	string Id,
	string Hostname,
	string Status) : ServerCoreResult(Id, Hostname, Status)
{
}
