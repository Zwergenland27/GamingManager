using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects;

public static class Converters
{
	public static ShortenedProjectDto ToDto(this Project project)
	{
		return new ShortenedProjectDto(
			project.Id.Value.ToString(),
			project.Name.Value);
	}
}
