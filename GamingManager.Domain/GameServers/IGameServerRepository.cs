using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.GameServers;

public interface IGameServerRepository
{
	void Add(GameServer gameServer);

	void Delete(GameServer gameServer);

	Task<GameServer?> GetAsync(GameServerId id);

	Task<bool> IsServerNameUniqeAsync(ServerName serverName);

	Task<GameServer?> GetAsync(ServerName serverName);

	IAsyncEnumerable<GameServer> GetAllStartablesAsync(ServerId serverId);
}
