using GamingManager.Application.Features.Accounts;
using GamingManager.Application.Features.Games;
using GamingManager.Application.Features.Users;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Contracts.Features.GameServers.DTOs;
using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Games;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GamingManager.Infrastructure.Repositories;

internal static class Common
{
	internal static IAsyncEnumerable<ShortenedAccountDto> GetAccountsAsync(this GamingManagerContext context, Expression<Func<Account, bool>> predicate)
	{
		return context.Accounts
			.AsNoTracking()
			.Where(predicate)
			.Select(account => account.ToDto())
			.AsAsyncEnumerable();
	}

	internal static IAsyncEnumerable<ShortenedUserDto> GetUsersAsync(this GamingManagerContext context, Expression<Func<User, bool>> predicate)
	{
		return context.Users
			.AsNoTracking()
			.Where(predicate)
			.Select(user => user.ToDto())
			.AsAsyncEnumerable();
	}

	internal static IAsyncEnumerable<ShortenedGameDto> GetGamesAsync(this GamingManagerContext context, Expression<Func<Game, bool>> predicate)
	{
		return context.Games
			.AsNoTracking()
			.Where(predicate)
			.Select(game => game.ToDto())
			.AsAsyncEnumerable();
	}
}
