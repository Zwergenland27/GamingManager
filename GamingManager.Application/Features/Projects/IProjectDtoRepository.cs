using GamingManager.Contracts.Features.Projects.Queries.Get;
using GamingManager.Contracts.Features.Projects.Queries.GetAll;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects;

public interface IProjectDtoRepository
{
	Task<GetProjectResult?> GetAsync(ProjectId projectId);

	IAsyncEnumerable<GetAllProjectsResult> GetAllAsync();
}
