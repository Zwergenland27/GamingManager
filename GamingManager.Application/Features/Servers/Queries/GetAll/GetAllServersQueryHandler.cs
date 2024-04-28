using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Servers.Queries.GetAll;

namespace GamingManager.Application.Features.Servers.Queries.GetAll;

public class GetAllServersQueryHandler(
	IServerDtoRepository serverDtoRepository) : IQueryHandler<GetAllServersQuery, IEnumerable<GetAllServersResult>>
{
	public async Task<CanFail<IEnumerable<GetAllServersResult>>> Handle(GetAllServersQuery request, CancellationToken cancellationToken)
	{
		return await serverDtoRepository.GetAllAsync().ToListAsync(cancellationToken);
	}
}
