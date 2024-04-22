using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Games;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Accounts.Queries.GetAll;

public class GetAllAccountsQueryHandler(
	IGameDtoRepository gameDtoRepository,
	IAccountDtoRepository accountDtoRepository) : IQueryHandler<GetAllAccountsQuery, IEnumerable<ShortenedAccountDto>>
{
	public async Task<CanFail<IEnumerable<ShortenedAccountDto>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
	{
		var gameId = await gameDtoRepository.GetIdAsync(request.GameName);
		if (gameId is null) return Errors.Games.NameNotFound;

		return await accountDtoRepository.GetAllAsync(gameId).ToListAsync(cancellationToken);
	}
}
