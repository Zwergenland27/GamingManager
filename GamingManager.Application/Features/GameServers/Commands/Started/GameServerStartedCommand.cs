using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.Commands.Started;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.Started;

public class GameServerStartedCommandBuilder : IRequestBuilder<GameServerStartedParameters, GameServerStartedCommand>
{
	public ValidatedRequiredProperty<GameServerStartedCommand> Configure(RequiredPropertyBuilder<GameServerStartedParameters, GameServerStartedCommand> builder)
	{
		var id = builder.ClassProperty(r => r.Name)
			.Required(Errors.GameServer.Started.NameMissing)
			.Map(p => p.Name, value => new GameServerName(value));

		return builder.Build(() => new GameServerStartedCommand(id));
	}
}

public record GameServerStartedCommand(GameServerName Name) : ICommand;
