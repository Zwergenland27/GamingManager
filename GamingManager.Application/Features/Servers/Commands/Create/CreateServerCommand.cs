using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Servers.Commands;
using GamingManager.Contracts.Features.Servers.DTOs;
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

		return builder.Build(() => new CreateServerCommand(hostname, address, mac));
	}
}

public record CreateServerCommand(
	Hostname Hostname,
	Uri Address,
	Mac Mac) : ICommand<DetailedServerDto>
{
}
