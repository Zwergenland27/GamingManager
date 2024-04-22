using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServers.DTOs;

namespace GamingManager.Application.Features.GameServers.Queries.GetAll;

public class GetAllGameServersQueryHandler(
	IGameServerDtoRepository gameServerDtoRepository) : IQueryHandler<GetAllGameServersQuery, IEnumerable<ShortenedGameServerDto>>
{
	public async Task<CanFail<IEnumerable<ShortenedGameServerDto>>> Handle(GetAllGameServersQuery request, CancellationToken cancellationToken)
	{
		return await gameServerDtoRepository.GetAllAsync().ToListAsync(cancellationToken);
	}
}
