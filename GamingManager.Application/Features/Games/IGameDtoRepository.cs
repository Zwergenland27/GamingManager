using GamingManager.Application.Features.Games.DTOs;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games;

public interface IGameDtoRepository
{
	IAsyncEnumerable<ShortenedGameDto> GetAllAsync();
	Task<GameId> GetIdAsync(GameName gameName);
}
