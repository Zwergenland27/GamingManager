using GamingManager.Domain.Projects;
using GamingManager.Domain.Projects.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class ProjectRepository(GamingManagerContext context) : IProjectRepository
{
	private readonly GamingManagerContext _context = context;

	public void Add(Project project)
	{
		_context.Projects.Add(project);
	}

	public void Delete(Project project)
	{
		_context.Projects.Remove(project);
	}

	public async Task<Project?> GetAsync(ProjectId id)
	{
		return await _context.Projects
			.Include(project => project.Team)
			.Include(project => project.Participants)
				.ThenInclude(participant => participant.Sessions
				.OrderBy(session => session.Start)
				.LastOrDefault())
			.Include(project => project.Participants)
				.ThenInclude(participant => participant.Bans)
			.FirstOrDefaultAsync(project => project.Id == id);
	}

	public async Task<Project?> GetAsync(ProjectName name)
	{
		return await _context.Projects
			.Include(project => project.Team)
			.Include(project => project.Participants)
				.ThenInclude(participant => participant.Sessions
				.OrderBy(session => session.Start)
				.LastOrDefault())
			.Include(project => project.Participants)
				.ThenInclude(participant => participant.Bans)
			.FirstOrDefaultAsync(project => project.Name == name);
	}
}
