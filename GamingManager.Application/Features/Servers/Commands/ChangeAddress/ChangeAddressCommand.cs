using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Servers.Commands;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.ChangeAddress;

public class ChangeAddressCommandBuilder : IRequestBuilder<ChangeServerAddressParameters, ChangeAddressCommand>
{
	public ValidatedRequiredProperty<ChangeAddressCommand> Configure(RequiredPropertyBuilder<ChangeServerAddressParameters, ChangeAddressCommand> builder)
	{
		var hostname = builder.ClassProperty(r => r.Hostname)
			.Required(Errors.Server.ChangeServerAddress.HostnameMissing)
			.Map(p => p.Hostname, Hostname.Create);

		var address = builder.ClassProperty(r => r.Address)
			.Required(Errors.Server.ChangeServerAddress.AddressMissing)
			.Map(p => p.Address, Converters.CreateUri);

		return builder.Build(() => new ChangeAddressCommand(hostname, address));
	}
}

public record ChangeAddressCommand(Hostname Hostname, Uri Address) : ICommand;
