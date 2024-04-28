using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.Commands.UseServer;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.UseServer;

public class UseServerCommandBuilder : IRequestBuilder<UseServerParameters, UseServerCommand>
{
	public ValidatedRequiredProperty<UseServerCommand> Configure(RequiredPropertyBuilder<UseServerParameters, UseServerCommand> builder)
	{
		var name = builder.ClassProperty(r => r.Name)
			.Required(Errors.GameServer.UseServer.NameMissing)
			.Map(p => p.Name, value => new GameServerName(value));

		var hostname = builder.ClassProperty(r => r.Hostname)
			.Required(Errors.GameServer.UseServer.HostnameMissing)
			.Map(p => p.Hostname, value => new Hostname(value));

		return builder.Build(() => new UseServerCommand(name, hostname));
	}
}

public record UseServerCommand(GameServerName Name, Hostname Hostname) : ICommand<UseServerResult>;