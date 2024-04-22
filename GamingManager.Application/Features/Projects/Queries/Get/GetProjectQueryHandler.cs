using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Projects.Queries.Get;

public class GetProjectQueryHandler(
	IProjectDtoRepository projectDtoRepository) : IQueryHandler<GetProjectQuery, DetailedProjectDto>
{
	public async Task<CanFail<DetailedProjectDto>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
	{
		var project = await projectDtoRepository.GetDetailedAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		return project;
	}
}
