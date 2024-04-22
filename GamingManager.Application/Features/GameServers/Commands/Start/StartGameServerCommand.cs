using GamingManager.Application.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.Start;

public record StartGameServerCommand(GameServerName GameServerName) : ICommand;
