using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Servers.Commands;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.Start;

public class StartServerCommandBuilder : IRequestBuilder<StartServerParameters, StartServerCommand>
{
	public ValidatedRequiredProperty<StartServerCommand> Configure(RequiredPropertyBuilder<StartServerParameters, StartServerCommand> builder)
	{
		var hostname = builder.ClassProperty(r => r.Hostname)
			.Required(Errors.Server.Start.HostnameMissing)
			.Map(p => p.Hostname, Hostname.Create);

		return builder.Build(() => new StartServerCommand(hostname));
	}
}

public record StartServerCommand(Hostname Hostname) : ICommand;
