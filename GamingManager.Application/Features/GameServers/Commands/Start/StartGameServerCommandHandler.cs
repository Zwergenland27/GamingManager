using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Commands.Start;

public class StartGameServerCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerRepository gameServerRepository,
	IServerRepository serverRepository) : ICommandHandler<StartGameServerCommand>
{
	public async Task<CanFail> Handle(StartGameServerCommand request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(request.GameServerName);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		if (gameServer.HostedOn is null) return Errors.GameServers.ServerNotHosted;

		var server = await serverRepository.GetAsync(gameServer.HostedOn);
		if (server is null) return Errors.Servers.IdNotFound;

		if (server.Status == ServerStatus.Online)
		{
			var startGameServerResult = gameServer.Start();
			if (startGameServerResult.HasFailed) return startGameServerResult.Errors;

			if (server.Maintenance) return Errors.Servers.NotStartable;
		}
		else
		{
			var startGameServerResult = gameServer.MarkForStart();
			if (startGameServerResult.HasFailed) return startGameServerResult.Errors;

			var startServerResult = server.Start();
			if (startServerResult.HasFailed) return startServerResult.Errors;
		}

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
