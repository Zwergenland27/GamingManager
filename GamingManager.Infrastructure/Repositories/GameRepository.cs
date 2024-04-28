using GamingManager.Domain.Games;
using GamingManager.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class GameRepository(GamingManagerDomainContext context) : IGameRepository
{
	public void Add(Game game)
	{
		context.Games.Add(game);
	}

	public void Delete(Game game)
	{
		context.Games.Remove(game);
	}

	public async Task<Game?> GetAsync(GameId id)
	{
		return await context.Games.FirstOrDefaultAsync(game => game.Id == id);
	}

	public async Task<Game?> GetAsync(GameName name)
	{
		return await context.Games.FirstOrDefaultAsync(game => game.Name == name);
	}

	public async Task<bool> IsNameUnique(GameName name)
	{
		return !await context.Games.AnyAsync(game => game.Name == name);
	}
}
