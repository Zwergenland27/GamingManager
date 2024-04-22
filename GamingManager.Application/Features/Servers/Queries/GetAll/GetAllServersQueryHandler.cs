using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Servers.DTOs;

namespace GamingManager.Application.Features.Servers.Queries.GetAll;

public class GetAllServersQueryHandler(
	IServerDtoRepository serverDtoRepository) : IQueryHandler<GetAllServersQuery, IEnumerable<ShortenedServerDto>>
{
	public async Task<CanFail<IEnumerable<ShortenedServerDto>>> Handle(GetAllServersQuery request, CancellationToken cancellationToken)
	{
		return await serverDtoRepository.GetAllAsync().ToListAsync(cancellationToken);
	}
}
