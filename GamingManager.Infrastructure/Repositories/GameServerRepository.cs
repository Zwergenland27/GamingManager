using GamingManager.Domain.GameServers;
using GamingManager.Domain.GameServers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace GamingManager.Infrastructure.Repositories;

public class GameServerRepository(GamingManagerContext context) : IGameServerRepository
{
	private readonly GamingManagerContext _context = context;

	public void Add(GameServer gameServer)
	{
		_context.GameServers.Add(gameServer);
	}

	public void Delete(GameServer gameServer)
	{
		_context.GameServers.Remove(gameServer);
	}

	public async Task<GameServer?> GetAsync(GameServerId id)
	{
		return await _context.GameServers.FirstOrDefaultAsync(gameServer => gameServer.Id == id);
	}
}
