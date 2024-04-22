using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServers.DTOs;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Events.Create;

public record CreateGameServerCommand(
	GameServerName GameServerName,
	ProjectName ProjectName,
	GameServerAutoShutdownDelay SutdownDelay) : ICommand<ShortenedGameServerDto>;
