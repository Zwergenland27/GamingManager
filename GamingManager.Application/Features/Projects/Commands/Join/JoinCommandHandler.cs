using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.Join;

public class JoinCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IGameServerRepository gameServerRepository,
	IProjectRepository projectRepository) : ICommandHandler<JoinCommand>
{
	public async Task<CanFail> Handle(JoinCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		if(project.ServerId != request.GameServerId) return Errors.Projects.WrongGameServer;

		var gameServer = await gameServerRepository.GetAsync(request.GameServerId);
		if (gameServer is null) return Errors.GameServers.IdNotFound;

		if (gameServer.Maintenance) return Errors.GameServers.InMaintenance;

		var account = await accountRepository.GetAsync(project.GameId, request.Uuid);
		if (account is null) return Errors.Accounts.IdNotFound;

		var result = project.Join(account.Id, request.JoinTimeUtc);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
