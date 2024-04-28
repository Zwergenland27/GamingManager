using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Games;
using GamingManager.Contracts.Features.Accounts.Queries.GetAllOfGame;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Queries.GetAll;

public class GetAllAccountsQueryHandler(
	IGameDtoRepository gameDtoRepository,
	IAccountDtoRepository accountDtoRepository) : IQueryHandler<GetAllAccountsQuery, IEnumerable<GetAllAccountsResult>>
{
	public async Task<CanFail<IEnumerable<GetAllAccountsResult>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
	{
		var gameId = await gameDtoRepository.GetIdAsync(request.GameName);
		if (gameId is null) return Errors.Games.NameNotFound;

		return await accountDtoRepository.GetAllAsync(new GameId(gameId.Value)).ToListAsync(cancellationToken);
	}
}
