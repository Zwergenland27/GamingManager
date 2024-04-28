using GamingManager.Contracts.Features.Projects;

namespace GamingManager.Contracts.Features.Users.Queries.Get;

public class GetUserProjectsResult(
	string Id,
	string Name) : ProjectCoreResult(Id, Name)
{
}
