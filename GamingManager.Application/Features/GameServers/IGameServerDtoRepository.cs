using GamingManager.Contracts.Features.GameServers.Queries.Get;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers;

public interface IGameServerDtoRepository
{
	Task<GetGameServerResult?> GetAsync(GameServerName gameServerName);
}
