using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts;

public interface IAccountDtoRepository
{
	IAsyncEnumerable<ShortenedAccountDto> GetAllAsync(GameId gameId);

	Task<DetailedAccountDto?> GetDetailedAsync(AccountId accountId);

	Task<DetailedAccountDto?> GetDetailedAsync(GameId gameId, AccountName name);
}
