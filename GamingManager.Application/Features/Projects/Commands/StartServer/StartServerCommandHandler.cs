using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Servers.ValueObjects;
using MediatR;

namespace GamingManager.Application.Features.Projects.Commands.StartServer;

public class StartServerCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository,
	IGameServerRepository gameServerRepository,
	IServerRepository serverRepository) : ICommandHandler<StartProjectServerCommand>
{
	public async Task<CanFail> Handle(StartProjectServerCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		if (!project.CanStartServer(request.AuditorId)) return Errors.Projects.Members.Forbidden;

		if (project.ServerId is null) return Errors.Projects.NoServer;

		var gameServer = await gameServerRepository.GetAsync(project.ServerId);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		if (gameServer.HostedOnId is null) return Errors.GameServers.ServerNotHosted;

		var server = await serverRepository.GetAsync(gameServer.HostedOnId);
		if (server is null) return Errors.Servers.IdNotFound;

		if (server.Maintenance) return Errors.Servers.NotStartable;
		if (gameServer.Maintenance) return Errors.GameServers.NotStartable;

		if (server.Status == ServerStatus.Online)
		{
			var startGameServerResult = gameServer.Start();
			if (startGameServerResult.HasFailed) return startGameServerResult.Errors;
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
