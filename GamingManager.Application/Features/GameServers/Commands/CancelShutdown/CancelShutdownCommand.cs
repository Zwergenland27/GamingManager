using GamingManager.Application.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Events.CancelShutdown;

public record CancelShutdownCommand(GameServerName GameServerName) : ICommand;
