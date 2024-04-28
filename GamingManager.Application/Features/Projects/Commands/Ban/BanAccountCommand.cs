using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects;
using GamingManager.Contracts.Features.Projects.Commands.Ban;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Ban;

public class BanAccountCommandBuilder : IRequestBuilder<BanAccountParameters, BanAccountCommand>
{
	public ValidatedRequiredProperty<BanAccountCommand> Configure(RequiredPropertyBuilder<BanAccountParameters, BanAccountCommand> builder)
	{
		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.BanAccount.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var accountId = builder.ClassProperty(r => r.AccountId)
			.Required(Errors.Project.BanAccount.AccountIdMissing)
			.Map(p => p.AccountId, AccountId.Create);

		var reason = builder.ClassProperty(r => r.Reason)
			.Required(Errors.Project.BanAccount.ReasonMissing)
			.Map(p => p.Reason, Reason.Create);

		var duration = builder.StructProperty(r => r.Duration)
			.Required(Errors.Project.BanAccount.DurationMissing)
			.Map(p => p.Duration);

		return builder.Build(() => new BanAccountCommand(projectId, accountId, reason, duration));
	}
}

public record BanAccountCommand(
	ProjectId ProjectId,
	AccountId AccountId,
	Reason Reason,
	TimeSpan? Duration) : ICommand<BanResult>;
