using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Projects.DTOs;

namespace GamingManager.Application.Features.Projects.Queries.GetAll;

public class GetAllProjectsQueryHandler(
	IProjectDtoRepository projectDtoRepository) : IQueryHandler<GetAllProjectsQuery, IEnumerable<ShortenedProjectDto>>
{
	public async Task<CanFail<IEnumerable<ShortenedProjectDto>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
	{
		return await projectDtoRepository.GetAllAsync().ToListAsync(cancellationToken);
	}
}
