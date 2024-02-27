using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Domain.Accounts;

public interface IAccountRepository
{
	void Add(Account account);

	void Delete(Account account);

	Task<Account?> GetAsync(AccountId accountId);

	Task<Account?> GetAsync(GameId gameId, Uuid uuid);

	Task<Account?> GetAsync(GameId gameId, AccountName name);
}
