using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.AllowAccount;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Allow;

public class AllowAccountOnProjectCommandBuilder : IRequestBuilder<AllowAccountParameters, AllowAccountOnProjectCommand>
{
	public ValidatedRequiredProperty<AllowAccountOnProjectCommand> Configure(RequiredPropertyBuilder<AllowAccountParameters, AllowAccountOnProjectCommand> builder)
	{
		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.AllowAccount.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var accountId = builder.ClassProperty(r => r.AccountId)
			.Required(Errors.Project.AllowAccount.AccountIdMissing)
			.Map(p => p.AccountId, AccountId.Create);

		return builder.Build(() => new AllowAccountOnProjectCommand(projectId, accountId));
	}
}

public record AllowAccountOnProjectCommand(
	ProjectId ProjectId,
	AccountId AccountId) : ICommand<AllowAccountResult>;
