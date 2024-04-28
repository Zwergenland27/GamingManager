using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServers.Commands.Create;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.GameServers.Events.Create;

public class CreateGameServerCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository,
	IGameServerRepository gameServerRepository) : ICommandHandler<CreateGameServerCommand, CreateGameServerResult>
{
	public async Task<CanFail<CreateGameServerResult>> Handle(CreateGameServerCommand request, CancellationToken cancellationToken)
	{
		var nameUnique = await gameServerRepository.IsServerNameUniqeAsync(request.GameServerName);
		if (!nameUnique) return Errors.GameServers.DuplicateServerName;

		var project = await projectRepository.GetAsync(request.ProjectName);
        if (project is null) return Errors.Projects.NameNotFound;

        var gameServerResult = GameServer.Create(project, request.GameServerName, request.ShutdownDelay);
		if(gameServerResult.HasFailed) return gameServerResult.Errors;

		gameServerRepository.Add(gameServerResult.Value);

		await unitOfWork.SaveAsync(cancellationToken);

		return new CreateGameServerResult(
			Id: gameServerResult.Value.Id.Value.ToString(),
			Name: gameServerResult.Value.ServerName.Value,
			Status: gameServerResult.Value.Status.ToString(),
			ShutdownDelay: gameServerResult.Value.ShutdownDelay.Minutes,
			Project: new CreateGameServerProjectResult(
				Id: project.Id.Value.ToString(),
				Name: project.Name.Value));
	}
}
