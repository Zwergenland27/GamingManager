using GamingManager.Application.Features.Games;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Contracts.Features.Projects.DTOs;
using GamingManager.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class GameDtoRepository(GamingManagerContext context) : IGameDtoRepository
{
	public IAsyncEnumerable<ShortenedGameDto> GetAllAsync()
	{
		return context.GetGamesAsync(game => true);
	}

	public async Task<DetailedGameDto?> GetDetailedAsync(GameName gameName)
	{
		//TODO: Refactor for better performance
		var game = await context.Games
			.AsNoTracking()
			.FirstOrDefaultAsync(game => game.Name == gameName);

		if(game is null) return null;

		var accounts = await context.GetAccountsAsync(account => account.Game == game.Id).ToListAsync();

		var projects = await context.Projects
			.AsNoTracking()
			.Where(project => project.Game == game.Id)
			.Select(project => new ShortenedProjectForGameDto(
				project.Id.Value.ToString(),
				project.Name.Value))
			.ToListAsync();
		return new DetailedGameDto(
			game.Id.Value.ToString(),
			game.Name.Value,
			accounts,
			projects);
	}

	public async Task<GameId?> GetIdAsync(GameName gameName)
	{
		return await context.Games
			.AsNoTracking()
			.Where(game => game.Name == gameName)
			.Select(game => game.Id)
			.FirstOrDefaultAsync();
	}
}
