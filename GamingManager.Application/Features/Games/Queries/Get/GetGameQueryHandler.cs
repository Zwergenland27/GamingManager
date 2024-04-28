using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Games.Queries.Get;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Games.Queries.Get;

public class GetGameQueryHandler(
	IGameDtoRepository gameDtoRepository) : IQueryHandler<GetGameQuery, GetGameResult>
{
	public async Task<CanFail<GetGameResult>> Handle(GetGameQuery request, CancellationToken cancellationToken)
	{
		var game = await gameDtoRepository.GetAsync(request.Name);
		if (game is null) return Errors.Games.NameNotFound;

		return game;
	}
}
