using GamingManager.Application.Features.GameServers.DTOs;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers;

public interface IGameServerDtoRepository
{
	Task<DetailedGameServerDto?> GetDetailedAsync(ServerName serverName);
}
