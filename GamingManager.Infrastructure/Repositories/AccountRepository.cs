﻿using GamingManager.Domain.Accounts;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class AccountRepository(GamingManagerContext context) : IAccountRepository
{
	private readonly GamingManagerContext _context = context;

	public void Add(Account account)
	{
		_context.Accounts.Add(account);
	}

	public void Delete(Account account)
	{
		_context.Remove(account);
	}

	public async Task<Account?> GetAsync(AccountId accountId)
	{
		return await _context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
	}

	public async Task<Account?> GetAsync(GameId gameId, Uuid uuid)
	{
		return await _context.Accounts.FirstOrDefaultAsync(account => account.Game == gameId && account.Uuid == uuid);
	}

	public async Task<Account?> GetAsync(GameId gameId, AccountName name)
	{
		return await _context.Accounts.FirstOrDefaultAsync(account => account.Game == gameId && account.Name == name);
	}
}
