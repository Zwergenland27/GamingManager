using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Pardon;

public class PardonAccountBuilder : IRequestBuilder<PardonAccountParameters, PardonAccountCommand>
{
	public ValidatedRequiredProperty<PardonAccountCommand> Configure(RequiredPropertyBuilder<PardonAccountParameters, PardonAccountCommand> builder)
	{
		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.Pardon.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var accountId = builder.ClassProperty(r => r.AccountId)
			.Required(Errors.Project.Pardon.AccountIdMissing)
			.Map(a => a.AccountId, AccountId.Create);

		return builder.Build(() => new PardonAccountCommand(projectId, accountId));
	}
}

public record PardonAccountCommand(
	ProjectId ProjectId,
	AccountId AccountId) : ICommand;
