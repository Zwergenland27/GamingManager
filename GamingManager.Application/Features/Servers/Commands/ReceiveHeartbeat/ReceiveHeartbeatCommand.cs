using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Servers.Commands.ReceiveHeartbeat;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.ReceiveHeartbeat;

public class ReceiveHeartbeatCommandBuilder : IRequestBuilder<ReceiveHeartbeatParameters, ReceiveHeartbeatCommand>
{
	public ValidatedRequiredProperty<ReceiveHeartbeatCommand> Configure(RequiredPropertyBuilder<ReceiveHeartbeatParameters, ReceiveHeartbeatCommand> builder)
	{
		var hostname = builder.ClassProperty(r => r.Hostname)
			.Required(Errors.Server.ReceiveHeartbeat.HostnameMissing)
			.Map(p => p.Hostname, Hostname.Create);

		return builder.Build(() => new ReceiveHeartbeatCommand(hostname));
	}
}

public record ReceiveHeartbeatCommand(Hostname Hostname) : ICommand;
