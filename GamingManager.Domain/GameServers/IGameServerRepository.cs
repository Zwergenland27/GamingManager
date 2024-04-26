using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.GameServers;

public interface IGameServerRepository
{
	void Add(GameServer gameServer);

	void Delete(GameServer gameServer);

	Task<GameServer?> GetAsync(GameServerId id);

	Task<bool> IsServerNameUniqeAsync(GameServerName serverName);

	Task<GameServer?> GetAsync(GameServerName serverName);

	IAsyncEnumerable<GameServer> GetAllStartablesAsync(ServerId serverId);

	IAsyncEnumerable<GameServer> GetAllOnlineAsync(ServerId serverId);

	IAsyncEnumerable<GameServer> GetAllOfServerAsync(ServerId serverId);
}
