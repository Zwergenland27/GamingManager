using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects;

public interface IProjectRepository
{
	void Add(Project project);

	void Delete(Project project);

	Task<Project?> GetAsync(ProjectId id);

	Task<Project?> GetAsync(ProjectName name);
}
