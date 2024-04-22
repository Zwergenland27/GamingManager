using GamingManager.Application.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.Started;

public record GameServerStartedCommand(GameServerId GameServerId) : ICommand;
