using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games.DTOs;

public record ShortenedGameDto(
	GameId Id,
	GameName Name)
{
}
