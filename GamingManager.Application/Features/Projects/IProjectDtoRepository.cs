using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects;

public interface IProjectDtoRepository
{
	Task<DetailedProjectDto?> GetAsync(ProjectName projectName);
}
