using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;

namespace GamingManager.Application.Features.GameServers.Commands.Started;

public class GameServerStartedCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerRepository gameServerRepository) : ICommandHandler<GameServerStartedCommand>
{
	public async Task<CanFail> Handle(GameServerStartedCommand request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(request.GameServerName);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		gameServer.Started();

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
