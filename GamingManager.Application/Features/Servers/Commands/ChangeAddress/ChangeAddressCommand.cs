using GamingManager.Application.Abstractions;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.ChangeAddress;

public record ChangeAddressCommand(Hostname Hostname, Address Address) : ICommand;
