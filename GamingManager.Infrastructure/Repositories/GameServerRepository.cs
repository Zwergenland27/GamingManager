using GamingManager.Domain.GameServers;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Servers.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class GameServerRepository(GamingManagerDomainContext context) : IGameServerRepository
{
	public void Add(GameServer gameServer)
	{
		context.GameServers.Add(gameServer);
	}

	public void Delete(GameServer gameServer)
	{
		context.GameServers.Remove(gameServer);
	}

	public IAsyncEnumerable<GameServer> GetAllOfServerAsync(ServerId serverId)
	{
		return context.GameServers.Where(gameServer => gameServer.HostedOnId == serverId).AsAsyncEnumerable();
	}

	public IAsyncEnumerable<GameServer> GetAllOnlineAsync(ServerId serverId)
	{
		return context.GameServers.Where(gameServer => gameServer.HostedOnId == serverId && gameServer.Status == GameServerStatus.Online).AsAsyncEnumerable();
	}

	public IAsyncEnumerable<GameServer> GetAllStartablesAsync(ServerId serverId)
	{
		return context.GameServers.Where(gameServer => gameServer.HostedOnId == serverId && gameServer.Status != GameServerStatus.WaitingForHardware).AsAsyncEnumerable();
	}

	public async Task<GameServer?> GetAsync(GameServerId id)
	{
		return await context.GameServers.FirstOrDefaultAsync(gameServer => gameServer.Id == id);
	}

	public async Task<GameServer?> GetAsync(GameServerName serverName)
	{
		return await context.GameServers.FirstOrDefaultAsync(gameServer => gameServer.ServerName == serverName);
	}

	public async Task<bool> IsServerNameUniqeAsync(GameServerName serverName)
	{
		return !await context.GameServers.AnyAsync(gameServer => gameServer.ServerName == serverName);
	}
}
