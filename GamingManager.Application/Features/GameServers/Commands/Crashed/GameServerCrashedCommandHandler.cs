using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;

namespace GamingManager.Application.Features.GameServers.Events.Crashed;

public class GameServerCrashedCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerRepository gameServerRepository) : ICommandHandler<GameServerCrashedCommand>
{
	public async Task<CanFail> Handle(GameServerCrashedCommand request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(request.ServerName);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		gameServer.Crashed(request.CrashedAtUtc);

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
