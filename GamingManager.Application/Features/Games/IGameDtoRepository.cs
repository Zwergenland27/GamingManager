using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games;

public interface IGameDtoRepository
{
	IAsyncEnumerable<ShortenedGameDto> GetAllAsync();

	Task<DetailedGameDto?> GetDetailedAsync(GameName gameName);
	Task<GameId?> GetIdAsync(GameName gameName);
}
