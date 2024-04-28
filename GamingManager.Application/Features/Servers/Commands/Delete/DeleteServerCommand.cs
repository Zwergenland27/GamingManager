using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Servers.Commands.Delete;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.Delete;

public class DeleteServerCommandBuilder : IRequestBuilder<DeleteServerParameters, DeleteServerCommand>
{
	public ValidatedRequiredProperty<DeleteServerCommand> Configure(RequiredPropertyBuilder<DeleteServerParameters, DeleteServerCommand> builder)
	{
		var hostname = builder.ClassProperty(r => r.Hostname)
			.Required(Errors.Server.Delete.HostnameMissing)
			.Map(p => p.Hostname, Hostname.Create);

		return builder.Build(() => new DeleteServerCommand(hostname));
	}
}

public record DeleteServerCommand(Hostname Hostname) : ICommand;
