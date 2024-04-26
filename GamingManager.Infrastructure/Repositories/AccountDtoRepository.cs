using GamingManager.Application.Features.Accounts;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GamingManager.Infrastructure.Repositories;

public class AccountDtoRepository(GamingManagerContext context) : IAccountDtoRepository
{
	public IAsyncEnumerable<ShortenedAccountDto> GetAllAsync(GameId gameId)
	{
		return context.GetAccountsAsync(account => account.Game == gameId);
	}

	public async Task<DetailedAccountDto?> GetDetailedAsync(AccountId accountId)
	{
		return await GetDetailedAsync(account => account.Id == accountId);
	}

	public async Task<DetailedAccountDto?> GetDetailedAsync(GameId gameId, AccountName name)
	{
		return await GetDetailedAsync(account => account.Game == gameId && account.Name == name);
	}

	private async Task<DetailedAccountDto?> GetDetailedAsync(Expression<Func<Account, bool>> predicate)
	{
		//TODO: Refactor for better performance

		var account = await context.Accounts
			.AsNoTracking()
			.FirstOrDefaultAsync(predicate);

		if (account is null) return null;

		var user = await context.GetUsersAsync(user => user.Id == account.User)
			.FirstOrDefaultAsync();

		var game = await context.GetGamesAsync(game => game.Id == account.Game)
			.FirstAsync();

		var projects = await context.Projects
			.AsNoTracking()
			.IgnoreAutoIncludes()
			.Include(project => project.Participants)
			.Where(project => project.Participants.Any(participant => participant.Account == account.Id))
			.Select(project => new ShortenedProjectDto(
				project.Id.Value.ToString(),
				project.Name.Value,
				game))
			.ToListAsync();

		return new DetailedAccountDto(
			account.Id.Value.ToString(),
			account.Name.Value,
			account.Uuid?.Value,
			user,
			game,
			projects);
	}
}
