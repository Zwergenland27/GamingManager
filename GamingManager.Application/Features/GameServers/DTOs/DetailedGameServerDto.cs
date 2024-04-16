using GamingManager.Application.Features.Games.DTOs;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.DTOs;

public record DetailedGameServerDto(
	GameServerId Id,
	ServerName Name,
	ShortenedProjectDto Project)
{

}
