using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Projects.Queries.Get;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Projects.Queries.Get;

public class GetProjectQueryHandler(
	IProjectDtoRepository projectDtoRepository) : IQueryHandler<GetProjectQuery, GetProjectResult>
{
	public async Task<CanFail<GetProjectResult>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
	{
		var project = await projectDtoRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		return project;
	}
}
