using GamingManager.Application.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.ChangeShutdownDelay;

public record ChangeShutdownDelayCommand(ServerName ServerName, GameServerAutoShutdownDelay ShutdownDelay) : ICommand
