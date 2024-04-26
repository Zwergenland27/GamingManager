using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Domain.Games;

namespace GamingManager.Application.Features.Games;

public static class Converters
{
	public static ShortenedGameDto ToDto(this Game game)
	{
		return new ShortenedGameDto(
			game.Id.Value.ToString(),
			game.Name.Value);
	}
}
