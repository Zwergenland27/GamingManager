using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects;
using GamingManager.Contracts.Features.Projects.Commands.Ban;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Ban;

public class BanParticipantCommandBuilder : IRequestBuilder<BanParticipantParameters, BanParticipantCommand>
{
	public ValidatedRequiredProperty<BanParticipantCommand> Configure(RequiredPropertyBuilder<BanParticipantParameters, BanParticipantCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.BanAccount.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var accountId = builder.ClassProperty(r => r.ParticipantId)
			.Required(Errors.Project.BanAccount.ParticipantIdMissing)
			.Map(p => p.ParticipantId, ParticipantId.Create);

		var reason = builder.ClassProperty(r => r.Reason)
			.Required(Errors.Project.BanAccount.ReasonMissing)
			.Map(p => p.Reason, Reason.Create);

		var duration = builder.StructProperty(r => r.Duration)
			.Required(Errors.Project.BanAccount.DurationMissing)
			.Map(p => p.Duration);

		return builder.Build(() => new BanParticipantCommand(auditorId, projectId, accountId, reason, duration));
	}
}

public record BanParticipantCommand(
	UserId AuditorId,
	ProjectId ProjectId,
	ParticipantId ParticipantId,
	Reason Reason,
	TimeSpan? Duration) : ICommand<BanResult>;
