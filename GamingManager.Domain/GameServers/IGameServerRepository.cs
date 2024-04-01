using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Domain.GameServers;

public interface IGameServerRepository
{
	void Add(GameServer gameServer);

	void Delete(GameServer gameServer);

	Task<GameServer?> GetAsync(GameServerId id);
}
