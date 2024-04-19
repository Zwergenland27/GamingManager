using GamingManager.Application.Abstractions;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.Start;

public record StartServerCommand(Hostname Hostname) : ICommand;
