using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Servers.DTOs;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.Create;

public record CreateServerCommand(
	Hostname Hostname,
	Address Address,
	Mac Mac) : ICommand<DetailedServerDto>
{
}
