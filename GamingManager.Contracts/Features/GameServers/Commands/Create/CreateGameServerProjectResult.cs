using GamingManager.Contracts.Features.Projects;

namespace GamingManager.Contracts.Features.GameServers.Commands.Create;

public class CreateGameServerProjectResult(
	string Id,
	string Name) : ProjectCoreResult(Id, Name)
{
}
