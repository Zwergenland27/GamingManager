using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Servers.Commands;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.ChangeHostname;

public class ChangeHostnameCommandBuilder : IRequestBuilder<ChangeHostnameParameters, ChangeHostnameCommand>
{
	public ValidatedRequiredProperty<ChangeHostnameCommand> Configure(RequiredPropertyBuilder<ChangeHostnameParameters, ChangeHostnameCommand> builder)
	{
		var currentHostname = builder.ClassProperty(r => r.CurrentHostname)
			.Required(Errors.Server.ChangeHostname.CurrentHostnameMissing)
			.Map(p => p.CurrentHostname, Hostname.Create);

		var newHostname = builder.ClassProperty(r => r.NewHostname)
			.Required(Errors.Server.ChangeHostname.NewHostnameMissing)
			.Map(p => p.NewHostname, Hostname.Create);

		return builder.Build(() => new ChangeHostnameCommand(currentHostname, newHostname));
	}
}

public record ChangeHostnameCommand(
	Hostname CurrentHostname,
	Hostname NewHostname) : ICommand;
