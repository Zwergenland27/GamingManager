using CleanDomainValidation.Application;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.Commands;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.ShuttedDown;

public class GameServerShuttedDownCommandBuilder : IRequestBuilder<GameServerShuttedDownParameters, GameServerShuttedDownCommand>
{
	public ValidatedRequiredProperty<GameServerShuttedDownCommand> Configure(RequiredPropertyBuilder<GameServerShuttedDownParameters, GameServerShuttedDownCommand> builder)
	{
		var id = builder.ClassProperty(r => r.GameServerName)
			.Required(Errors.GameServer.ShuttedDown.NameMissing)
			.Map(p => p.GameServerName, value => new GameServerId(value));

		return builder.Build(() => new GameServerShuttedDownCommand(id));
	}
}

public record GameServerShuttedDownCommand(GameServerName GameServerName) : ICommand;
