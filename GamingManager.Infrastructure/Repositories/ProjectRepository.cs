using GamingManager.Domain.Projects;
using GamingManager.Domain.Projects.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class ProjectRepository(GamingManagerDomainContext context) : IProjectRepository
{
	public void Add(Project project)
	{
		context.Projects.Add(project);
	}

	public void Delete(Project project)
	{
		context.Projects.Remove(project);
	}

	public async Task<Project?> GetAsync(ProjectId id)
	{
		return await context.Projects
			.FirstOrDefaultAsync(project => project.Id == id);
	}

	public async Task<Project?> GetAsync(ProjectName name)
	{
		return await context.Projects
			.FirstOrDefaultAsync(project => project.Name == name);
	}
}
