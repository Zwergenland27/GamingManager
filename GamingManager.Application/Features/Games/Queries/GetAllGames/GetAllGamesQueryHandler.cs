using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Games.DTOs;

namespace GamingManager.Application.Features.Games.Queries.GetAllGames;

public class GetAllGamesQueryHandler(IGameDtoRepository gameDtoRepository) : IQueryHandler<GetAllGamesQuery, IEnumerable<ShortenedGameDto>>
{
	public async Task<CanFail<IEnumerable<ShortenedGameDto>>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
	{
		return await gameDtoRepository.GetAllAsync().ToListAsync(cancellationToken);
	}
}
