using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.Leave;

public class LeaveCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IProjectRepository projectRepository) : ICommandHandler<LeaveCommand>
{
	public async Task<CanFail> Handle(LeaveCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		if (project.ServerId != request.GameServerId) return Errors.Projects.WrongGameServer;

		var account = await accountRepository.GetAsync(project.GameId, request.Uuid);
		if (account is null) return Errors.Accounts.IdNotFound;

		var result = project.Leave(account.Id, request.LeaveTimeUtc);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
