using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.Commands;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Events.Delete;

public class DeleteGameServerCommandBuilder : IRequestBuilder<DeleteGameServerParameters, DeleteGameServerCommand>
{
	public ValidatedRequiredProperty<DeleteGameServerCommand> Configure(RequiredPropertyBuilder<DeleteGameServerParameters, DeleteGameServerCommand> builder)
	{
		var name = builder.ClassProperty(r => r.GameServerName)
			.Required(Errors.GameServer.Delete.NameMissing)
			.Map(p => p.GameServerName, value => new GameServerName(value));

		return builder.Build(() => new DeleteGameServerCommand(name));
	}
}
public record DeleteGameServerCommand(GameServerName GameServerName) : ICommand;
