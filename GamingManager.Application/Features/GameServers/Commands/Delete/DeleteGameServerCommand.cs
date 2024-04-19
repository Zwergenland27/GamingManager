using GamingManager.Application.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Events.Delete;

public record DeleteGameServerCommand(ServerName ServerName) : ICommand;
