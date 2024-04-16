using GamingManager.Application.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.CancelShutdown;

public record CancelShutdownCommand(ServerName ServerName) : ICommand;
