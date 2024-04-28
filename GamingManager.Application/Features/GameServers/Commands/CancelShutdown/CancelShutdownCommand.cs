using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.Commands.CancelShutdown;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Events.CancelShutdown;

public class CancelShutdownCommandBuilder : IRequestBuilder<CancelGameServerShutdownParameters, CancelShutdownCommand>
{
	public ValidatedRequiredProperty<CancelShutdownCommand> Configure(RequiredPropertyBuilder<CancelGameServerShutdownParameters, CancelShutdownCommand> builder)
	{
		var name = builder.ClassProperty(r => r.GameServerName)
			.Required(Errors.GameServer.CancelShutdown.NameMissing)
			.Map(p => p.GameServerName, value => new GameServerName(value));

		return builder.Build(() => new CancelShutdownCommand(name));
	}
}

public record CancelShutdownCommand(GameServerName GameServerName) : ICommand;
