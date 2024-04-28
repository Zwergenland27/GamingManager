using GamingManager.Contracts.Features.Projects;

namespace GamingManager.Contracts.Features.Accounts.Queries.Get;

public class GetAccountProjectsResult(
	string Id,
	string Name) : ProjectCoreResult(Id, Name)
{
}
