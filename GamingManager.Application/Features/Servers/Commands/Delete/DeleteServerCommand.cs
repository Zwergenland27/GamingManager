using GamingManager.Application.Abstractions;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.Delete;

public record DeleteServerCommand(Hostname Hostname) : ICommand;
