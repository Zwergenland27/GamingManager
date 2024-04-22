using GamingManager.Application.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.ShuttedDown;

public record GameServerShuttedDownCommand(GameServerId GameServerId) : ICommand;
