using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.Commands;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Events.Crashed;

public class GameServerCrashedCommandBuilder : IRequestBuilder<GameServerCrashedParameters, GameServerCrashedCommand>
{
	public ValidatedRequiredProperty<GameServerCrashedCommand> Configure(RequiredPropertyBuilder<GameServerCrashedParameters, GameServerCrashedCommand> builder)
	{
		var name = builder.ClassProperty(r => r.Name)
			.Required(Errors.GameServer.Crashed.NameMissing)
			.Map(p => p.GameServerName, value => new GameServerName(value));

		var crashedAtUtc = builder.ClassProperty(r => r.CrashedAtUtc)
			.Required(Errors.GameServer.Crashed.CrashedAtUtcMissing)
			.Map(p => p.CrashedAtUtc, value => new GameServerCrashedAtUtc(value));

		return builder.Build(() => new GameServerCrashedCommand(name, crashedAtUtc));
	}
}
public record GameServerCrashedCommand(GameServerName Name, GameServerCrashedAtUtc CrashedAtUtc) : ICommand;
