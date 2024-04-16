using GamingManager.Application.Abstractions;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.ReceiveHeartbeat;

public record ReceiveHeartbeatCommand(Hostname Hostname) : ICommand;
