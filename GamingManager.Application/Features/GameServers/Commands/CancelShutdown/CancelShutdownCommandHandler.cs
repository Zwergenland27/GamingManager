using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;

namespace GamingManager.Application.Features.GameServers.Events.CancelShutdown;

public class CancelShutdownCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerRepository gameServerRepository) : ICommandHandler<CancelShutdownCommand>
{
	public async Task<CanFail> Handle(CancelShutdownCommand request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(request.ServerName);
		if(gameServer is null) return Errors.GameServers.ServerNameNotFound;

		gameServer.CancelShutdown();

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
