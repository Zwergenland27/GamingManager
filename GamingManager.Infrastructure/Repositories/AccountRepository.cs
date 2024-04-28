using GamingManager.Domain.Accounts;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class AccountRepository(GamingManagerDomainContext context) : IAccountRepository
{
	public void Add(Account account)
	{
		context.Accounts.Add(account);
	}

	public void Delete(Account account)
	{
		context.Remove(account);
	}

	public async Task<Account?> GetAsync(AccountId accountId)
	{
		return await context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
	}

	public async Task<Account?> GetAsync(GameId gameId, Uuid uuid)
	{
		return await context.Accounts.FirstOrDefaultAsync(account => account.GameId == gameId && account.Uuid == uuid);
	}

	public async Task<Account?> GetAsync(GameId gameId, AccountName name)
	{
		return await context.Accounts.FirstOrDefaultAsync(account => account.GameId == gameId && account.Name == name);
	}
}
