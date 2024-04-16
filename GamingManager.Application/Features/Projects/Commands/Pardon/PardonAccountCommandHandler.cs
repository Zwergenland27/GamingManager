using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.Pardon;

public class PardonAccountCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IProjectRepository projectRepository) : ICommandHandler<PardonAccountCommand>
{
	public async Task<CanFail> Handle(PardonAccountCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		var account = await accountRepository.GetAsync(request.AccountId);
		if (account is null) return Errors.Accounts.IdNotFound;

		var result = project.Pardon(account);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
