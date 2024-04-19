using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.GameServers.DTOs;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.GameServers.Events.Create;

public class CreateGameServerCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository,
	IGameServerRepository gameServerRepository,
	IGameServerDtoRepository gameServerDtoRepository) : ICommandHandler<CreateGameServerCommand, DetailedGameServerDto>
{
	public async Task<CanFail<DetailedGameServerDto>> Handle(CreateGameServerCommand request, CancellationToken cancellationToken)
	{
		var nameUnique = await gameServerRepository.IsServerNameUniqeAsync(request.ServerName);
		if (!nameUnique) return Errors.GameServers.DuplicateServerName;

		var project = await projectRepository.GetAsync(request.ProjectName);
        if (project is null) return Errors.Projects.NameNotFound;

        var gameServerResult = GameServer.Create(project, request.ServerName, request.SutdownDelay);
		if(gameServerResult.HasFailed) return gameServerResult.Errors;

		gameServerRepository.Add(gameServerResult.Value);

		await unitOfWork.SaveAsync(cancellationToken);

		return (await gameServerDtoRepository.GetDetailedAsync(request.ServerName))!;
	}
}
