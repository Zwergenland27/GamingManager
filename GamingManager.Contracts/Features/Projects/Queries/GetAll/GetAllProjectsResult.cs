namespace GamingManager.Contracts.Features.Projects.Queries.GetAll;

public class GetAllProjectsResult(
	string Id,
	string Name) : ProjectCoreResult(Id, Name)
{
}
