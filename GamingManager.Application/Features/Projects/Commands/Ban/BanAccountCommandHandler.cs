using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.Ban;

public class BanAccountCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IProjectRepository projectRepository) : ICommandHandler<BanAccountCommand>
{
	public async Task<CanFail> Handle(BanAccountCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		var account = await accountRepository.GetAsync(request.AccountId);
		if (account is null) return Errors.Accounts.IdNotFound;

		if (request.Duration is null)
		{
			var result = project.BanPermanent(account, request.Reason);
			if (result.HasFailed) return result.Errors;
		}
		else
		{
			var result = project.BanTemporary(account, request.Reason, request.Duration.Value);
			if (result.HasFailed) return result.Errors;
		}

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
