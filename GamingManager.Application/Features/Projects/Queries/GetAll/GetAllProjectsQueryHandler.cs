using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Projects.Queries.GetAll;

namespace GamingManager.Application.Features.Projects.Queries.GetAll;

public class GetAllProjectsQueryHandler(
	IProjectDtoRepository projectDtoRepository) : IQueryHandler<GetAllProjectsQuery, IEnumerable<GetAllProjectsResult>>
{
	public async Task<CanFail<IEnumerable<GetAllProjectsResult>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
	{
		return await projectDtoRepository.GetAllAsync(request.AuditorId).ToListAsync(cancellationToken);
	}
}
