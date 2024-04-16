using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.GameServers.Commands.Delete;

public class DeleteGameServerCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerRepository gameServerRepository,
	IProjectRepository projectRepository) : ICommandHandler<DeleteGameServerCommand>
{
	public async Task<CanFail> Handle(DeleteGameServerCommand request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(request.ServerName);
		if(gameServer is null) return Errors.GameServers.ServerNameNotFound;

		var project = await projectRepository.GetAsync(gameServer.Project);
		if(project is null)
		{
			throw new InvalidOperationException("The project associated with the game server does not exist");
		}

		if (!project.Ended) return Errors.GameServers.ActiveProject;

		gameServerRepository.Delete(gameServer);
		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
