using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.Pardon;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Pardon;

public class PardonParticipantCommandBuilder : IRequestBuilder<PardonParticipantParameters, PardonParticipantCommand>
{
	public ValidatedRequiredProperty<PardonParticipantCommand> Configure(RequiredPropertyBuilder<PardonParticipantParameters, PardonParticipantCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.Pardon.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var participantId = builder.ClassProperty(r => r.ParticipantId)
			.Required(Errors.Project.Pardon.ParticipantIdMissing)
			.Map(a => a.ParticipantId, ParticipantId.Create);

		return builder.Build(() => new PardonParticipantCommand(auditorId, projectId, participantId));
	}
}

public record PardonParticipantCommand(
	UserId AuditorId,
	ProjectId ProjectId,
	ParticipantId ParticipantId) : ICommand;
