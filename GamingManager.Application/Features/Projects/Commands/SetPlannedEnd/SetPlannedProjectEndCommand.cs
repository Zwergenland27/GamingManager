using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.SetPlannedEnd;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.SetPlannedEnd;

public class SetPlannedProjectEndCommandBuilder : IRequestBuilder<SetPlannedEndParameters, SetPlannedProjectEndCommand>
{
	public ValidatedRequiredProperty<SetPlannedProjectEndCommand> Configure(RequiredPropertyBuilder<SetPlannedEndParameters, SetPlannedProjectEndCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.SetPlannedEnd.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var plannedEndUtc = builder.ClassProperty(r => r.PlannedEndUtc)
			.Required(Errors.Project.SetPlannedEnd.PlannedEndAtUtcMissing)
			.Map(p => p.PlannedEndUtc, value => new ProjectEndsAtUtc(value));

		return builder.Build(() => new SetPlannedProjectEndCommand(auditorId, projectId, plannedEndUtc));
	}
}

public record SetPlannedProjectEndCommand(
	UserId AuditorId,
	ProjectId ProjectId,
	ProjectEndsAtUtc PlannedEndUtc) : ICommand;
