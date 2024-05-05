using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.Commands.Start;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.Start;

public class StartGameServerCommandBuilder : IRequestBuilder<StartGameServerParameters, StartGameServerCommand>
{
	public ValidatedRequiredProperty<StartGameServerCommand> Configure(RequiredPropertyBuilder<StartGameServerParameters, StartGameServerCommand> builder)
	{
		var name = builder.ClassProperty(r => r.Name)
			.Required(Errors.GameServer.Start.NameMissing)
			.Map(p => p.Name, value => new GameServerName(value));

		return builder.Build(() => new StartGameServerCommand(name));
	}
}
public record StartGameServerCommand(GameServerName Name) : ICommand;
