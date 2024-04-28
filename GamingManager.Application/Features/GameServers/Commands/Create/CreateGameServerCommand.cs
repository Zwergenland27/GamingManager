using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.Commands.Create;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Events.Create;

public class CreateGameServerCommandBuilder : IRequestBuilder<CreateGameServerParameters, CreateGameServerCommand>
{
	public ValidatedRequiredProperty<CreateGameServerCommand> Configure(RequiredPropertyBuilder<CreateGameServerParameters, CreateGameServerCommand> builder)
	{
		var name = builder.ClassProperty(r => r.GameServerName)
			.Required(Errors.GameServer.Create.NameMissing)
			.Map(p => p.GameServerName, value => new GameServerName(value));

		var project = builder.ClassProperty(r => r.ProjectName)
			.Required(Errors.GameServer.Create.ProjectNameMissing)
			.Map(p => p.ProjectName, value => new ProjectName(value));

		var shutdownDelay = builder.ClassProperty(r => r.ShutdownDelay)
			.Required(Errors.GameServer.Create.ShutdownDelayMissing)
			.Map(p => p.ShutdownDelay, value => new GameServerAutoShutdownDelay(value));

		return builder.Build(() => new CreateGameServerCommand(name, project, shutdownDelay));
	}
}

public record CreateGameServerCommand(
	GameServerName GameServerName,
	ProjectName ProjectName,
	GameServerAutoShutdownDelay ShutdownDelay) : ICommand<CreateGameServerResult>;
