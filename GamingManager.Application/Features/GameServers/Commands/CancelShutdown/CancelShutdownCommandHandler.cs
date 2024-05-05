using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.GameServers.Events.CancelShutdown;

public class CancelShutdownCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerRepository gameServerRepository,
	IProjectRepository projectRepository) : ICommandHandler<CancelShutdownCommand>
{
	public async Task<CanFail> Handle(CancelShutdownCommand request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(request.GameServerName);
		if(gameServer is null) return Errors.GameServers.ServerNameNotFound;


        gameServer.CancelShutdown();

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
