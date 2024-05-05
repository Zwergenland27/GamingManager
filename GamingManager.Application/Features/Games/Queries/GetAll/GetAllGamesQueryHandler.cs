using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Games.Queries.GetAll;

namespace GamingManager.Application.Features.Games.Queries.GetAll;

public class GetAllGamesQueryHandler(IGameDtoRepository gameDtoRepository) : IQueryHandler<GetAllGamesQuery, IEnumerable<GetAllGamesResult>>
{
	public async Task<CanFail<IEnumerable<GetAllGamesResult>>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
	{
		return await gameDtoRepository.GetAllAsync().ToListAsync(cancellationToken);
	}
}
