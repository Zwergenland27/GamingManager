using GamingManager.Application.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Events.Crashed;

public record GameServerCrashedCommand(GameServerId GameServerId, GameServerCrashedAtUtc CrashedAtUtc) : ICommand;
