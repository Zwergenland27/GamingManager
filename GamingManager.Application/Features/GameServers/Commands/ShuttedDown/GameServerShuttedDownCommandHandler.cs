using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;

namespace GamingManager.Application.Features.GameServers.Commands.ShuttedDown;

public class GameServerShuttedDownCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerRepository gameServerRepository) : ICommandHandler<GameServerShuttedDownCommand>
{

	public async Task<CanFail> Handle(GameServerShuttedDownCommand request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(request.GameServerId);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		gameServer.ShuttedDown();

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
