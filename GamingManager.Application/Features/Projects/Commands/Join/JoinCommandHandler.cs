using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.Join;

public class JoinCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IProjectRepository projectRepository) : ICommandHandler<JoinCommand>
{
	public async Task<CanFail> Handle(JoinCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		//TODO: check that the gameServer of the project and the server are not in maintenance mode!

		var account = await accountRepository.GetAsync(project.Game, request.Uuid);
		if (account is null) return Errors.Accounts.IdNotFound;

		var result = project.Join(account, request.JoinTimeUtc);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
