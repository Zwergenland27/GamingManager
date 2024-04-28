using GamingManager.Application.Features.Accounts;
using GamingManager.Contracts.Features.Accounts.Queries.Get;
using GamingManager.Contracts.Features.Accounts.Queries.GetAllOfGame;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class AccountDtoRepository(GamingManagerReadContext context) : IAccountDtoRepository
{
	public IAsyncEnumerable<GetAllAccountsResult> GetAllAsync(GameId gameId)
	{
		return context.Accounts
			.Include(account => account.User)
			.Where(account => account.GameId == gameId.Value)
			.Select(account => new GetAllAccountsResult(
				account.Id.ToString(),
				account.Name,
				account.Uuid,
				ReferenceEquals(account.User, null) ? null : new GetAllAccountsUserResult(
					account.User.Id.ToString(),
					account.User.Username)))
			.AsAsyncEnumerable();
	}

	public async Task<GetAccountResult?> GetAsync(GameId gameId, AccountName name)
	{
		return await context.Accounts
			.Include(account => account.User)
			.Include(account => account.Participants)
				.ThenInclude(participant => participant.Project)
			.Where(account => account.GameId == gameId.Value && account.Name == name.Value)
			.Select(account => new GetAccountResult(
				account.Id.ToString(),
				account.Name,
				account.Uuid,
				ReferenceEquals(account.User, null) ? null : new GetAccountUserResult(
					account.User.Id.ToString(),
					account.User.Username),
				account.Verified,
				account.Participants.Select(participant => new GetAccountProjectsResult(
					participant.Project.Id.ToString(),
					participant.Project.Name))
				.ToList()))
			.FirstOrDefaultAsync();
	}
}
