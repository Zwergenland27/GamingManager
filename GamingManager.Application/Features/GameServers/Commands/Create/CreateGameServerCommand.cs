using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.GameServers.DTOs;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Events.Create;

public record CreateGameServerCommand(
	ServerName ServerName,
	ProjectName ProjectName,
	GameServerAutoShutdownDelay SutdownDelay) : ICommand<DetailedGameServerDto>;
