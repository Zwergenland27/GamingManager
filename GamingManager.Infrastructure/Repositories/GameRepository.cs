using GamingManager.Domain.Games;
using GamingManager.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GamingManager.Infrastructure.Repositories;

public class GameRepository(GamingManagerContext context) : IGameRepository
{
	private readonly GamingManagerContext _context = context;
	public void Add(Game game)
	{
		_context.Games.Add(game);
	}

	public void Delete(Game game)
	{
		_context.Games.Remove(game);
	}

	public async Task<Game?> GetAsync(GameId id)
	{
		return await _context.Games.FirstOrDefaultAsync(game => game.Id == id);
	}

	public async Task<Game?> GetAsync(GameName name)
	{
		return await _context.Games.FirstOrDefaultAsync(game => game.Name == name);
	}

	public async Task<bool> IsNameUsed(GameName name)
	{
		return await _context.Games.AnyAsync(game => game.Name == name);
	}
}
