using GamingManager.Contracts.Features.GameServers.DTOs;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers;

public interface IGameServerDtoRepository
{
	IAsyncEnumerable<ShortenedGameServerDto> GetAllAsync();
	Task<DetailedGameServerDto?> GetDetailedAsync(GameServerName gameServerName);
}
