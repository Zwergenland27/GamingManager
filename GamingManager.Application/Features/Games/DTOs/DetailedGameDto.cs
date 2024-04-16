using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Domain.Games;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games.DTOs;

public record DetailedGameDto(
	GameId Id,
	GameName Name)
{
	public static DetailedGameDto FromGame(Game game)
	{
		return new DetailedGameDto(
			game.Id,
			game.Name);
	}
	public DetailedGame Convert()
	{
		return new DetailedGame(
			Id.Value.ToString(),
			Name.Value);
	}
}
