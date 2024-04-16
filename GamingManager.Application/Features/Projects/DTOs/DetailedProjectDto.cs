using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.DTOs;

public record DetailedProjectDto(
	ProjectId projectId,
	ProjectName Name,
	)
{
}
