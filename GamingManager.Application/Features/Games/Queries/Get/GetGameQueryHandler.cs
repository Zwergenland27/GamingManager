using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Games.Queries.Get;

public class GetGameQueryHandler(
	IGameDtoRepository gameDtoRepository) : IQueryHandler<GetGameQuery, DetailedGameDto>
{
	public async Task<CanFail<DetailedGameDto>> Handle(GetGameQuery request, CancellationToken cancellationToken)
	{
		var game = await gameDtoRepository.GetDetailedAsync(request.Name);
		if (game is null) return Errors.Games.NameNotFound;

		return game;
	}
}
