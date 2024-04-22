using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Games;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Accounts.Queries.Get;

public class GetAccountQueryHandler(
	IGameDtoRepository gameDtoRepository,
	IAccountDtoRepository accountDtoRepository) : IQueryHandler<GetAccountQuery, DetailedAccountDto>
{
	public async Task<CanFail<DetailedAccountDto>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
	{
		var gameId = await gameDtoRepository.GetIdAsync(request.GameName);
		if (gameId is null) return Errors.Games.NameNotFound;

		var account = await accountDtoRepository.GetDetailedAsync(gameId, request.AccountName);
		if (account is null) return Errors.Accounts.NameNotFound;

		return account;
	}
}
