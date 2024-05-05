using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.Commands.ChangeShutdownDelay;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.ChangeShutdownDelay;

public class ChangeShutdownDelayCommandBuilder : IRequestBuilder<ChangeGameServerShutdownDelayParameters, ChangeShutdownDelayCommand>
{
	public ValidatedRequiredProperty<ChangeShutdownDelayCommand> Configure(RequiredPropertyBuilder<ChangeGameServerShutdownDelayParameters, ChangeShutdownDelayCommand> builder)
	{
		var name = builder.ClassProperty(r => r.GameServerName)
			.Required(Errors.GameServer.ChangeShutdownDelay.NameMissing)
			.Map(p => p.Name, value => new GameServerName(value));

		var shutdownDelay = builder.ClassProperty(r => r.ShutdownDelay)
			.Required(Errors.GameServer.ChangeShutdownDelay.DelayMissing)
			.Map(p => p.Delay, value => new GameServerAutoShutdownDelay(value));

		return builder.Build(() => new ChangeShutdownDelayCommand(name, shutdownDelay));
	}
}
public record ChangeShutdownDelayCommand(GameServerName GameServerName, GameServerAutoShutdownDelay ShutdownDelay) : ICommand;
