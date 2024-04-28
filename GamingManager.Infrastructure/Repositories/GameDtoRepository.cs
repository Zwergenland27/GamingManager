using GamingManager.Application.Features.Games;
using GamingManager.Contracts.Features.Games.Queries;
using GamingManager.Contracts.Features.Games.Queries.Get;
using GamingManager.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GamingManager.Infrastructure.Repositories;

public class GameDtoRepository(GamingManagerReadContext context) : IGameDtoRepository
{
	public IAsyncEnumerable<GetAllGamesResult> GetAllAsync()
	{
		return context.Games
			.Select(game => new GetAllGamesResult(
				game.Id.ToString(),
				game.Name))
			.AsAsyncEnumerable();
	}

	public async Task<GetGameResult?> GetAsync(GameName gameName)
	{
		return await context.Games
			.Include(game => game.Accounts)
			.Include(game => game.Projects)
			.Where(game => game.Name == gameName.Value)
			.Select(game => new GetGameResult(
				game.Id.ToString(),
				game.Name,
				game.Accounts.Select(account => new GetGameAccountsResult(
					account.Id.ToString(),
					account.Name))
				.ToList(),
				game.Projects.Select(project => new GetGameProjectsResult(
					project.Id.ToString(),
					project.Name))
				.ToList()))
			.FirstOrDefaultAsync();
	}

	public async Task<Guid?> GetIdAsync(GameName gameName)
	{
		return await context.Games
			.Where(game => game.Name == gameName.Value)
			.Select(game => game.Id)
			.FirstOrDefaultAsync();
	}
}
