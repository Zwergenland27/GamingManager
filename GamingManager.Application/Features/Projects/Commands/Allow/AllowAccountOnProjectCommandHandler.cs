using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Projects.Commands.AllowAccount;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.Allow;

public class AllowAccountOnProjectCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IProjectRepository projectRepository) : ICommandHandler<AllowAccountOnProjectCommand, AllowAccountResult>
{
	public async Task<CanFail<AllowAccountResult>> Handle(AllowAccountOnProjectCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		var account = await accountRepository.GetAsync(request.AccountId);
		if (account is null) return Errors.Accounts.IdNotFound;

		var result = project.Allow(account);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return new AllowAccountResult(
			Id: account.Id.Value.ToString(),
			Account: new AllowAccountAccountResult(
				Id: account.Id.Value.ToString(),
				Name: account.Name.Value,
				Uuid: account.Uuid?.Value),
			SinceUtc: DateTime.UtcNow);
	}
}
