using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Servers.Commands.Create;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.Create;

public class CreateServerCommandBuilder : IRequestBuilder<CreateServerParameters, CreateServerCommand>
{
	public ValidatedRequiredProperty<CreateServerCommand> Configure(RequiredPropertyBuilder<CreateServerParameters, CreateServerCommand> builder)
	{
		var hostname = builder.ClassProperty(r => r.Hostname)
			.Required(Errors.Server.CreateServer.HostnameMissing)
			.Map(p => p.Hostname, Hostname.Create);

		var address = builder.ClassProperty(r => r.Address)
			.Required(Errors.Server.CreateServer.AddressMissing)
			.Map(p => p.Address, Converters.CreateUri);

		var mac = builder.ClassProperty(r => r.Mac)
			.Required(Errors.Server.CreateServer.MacMissing)
			.Map(p => p.Mac, Mac.Create);

		var shutdownDelay = builder.ClassProperty(r => r.ShutdownDelay)
			.Required(Errors.Server.CreateServer.ShutdownDelayMissing)
			.Map(p => p.ShutdownDelay, value => new ServerAutoShutdownDelay(value));

		return builder.Build(() => new CreateServerCommand(hostname, address, mac, shutdownDelay));
	}
}

public record CreateServerCommand(
	Hostname Hostname,
	Uri Address,
	Mac Mac,
	ServerAutoShutdownDelay ShutdownDelay) : ICommand<CreateServerResult>
{
}
