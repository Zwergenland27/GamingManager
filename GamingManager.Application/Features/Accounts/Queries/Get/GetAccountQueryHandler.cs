using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Games;
using GamingManager.Contracts.Features.Accounts.Queries.Get;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Queries.Get;

public class GetAccountQueryHandler(
	IGameDtoRepository gameDtoRepository,
	IAccountDtoRepository accountDtoRepository) : IQueryHandler<GetAccountQuery, GetAccountResult>
{
	public async Task<CanFail<GetAccountResult>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
	{
		var gameId = await gameDtoRepository.GetIdAsync(request.GameName);
		if (gameId is null) return Errors.Games.NameNotFound;

		var account = await accountDtoRepository.GetAsync(new GameId(gameId.Value), request.AccountName);
		if (account is null) return Errors.Accounts.NameNotFound;

		return account;
	}
}
