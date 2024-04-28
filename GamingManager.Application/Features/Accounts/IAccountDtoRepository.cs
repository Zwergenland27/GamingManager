using GamingManager.Contracts.Features.Accounts.Queries.Get;
using GamingManager.Contracts.Features.Accounts.Queries.GetAllOfGame;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts;

public interface IAccountDtoRepository
{
	IAsyncEnumerable<GetAllAccountsResult> GetAllAsync(GameId gameId);

	Task<GetAccountResult?> GetAsync(GameId gameId, AccountName name);
}
