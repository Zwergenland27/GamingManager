using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.SetPlannedEnd;

public class SetPlannedProjectEndCommandBuilder : IRequestBuilder<SetPlannedEndParameters, SetPlannedProjectEndCommand>
{
	public ValidatedRequiredProperty<SetPlannedProjectEndCommand> Configure(RequiredPropertyBuilder<SetPlannedEndParameters, SetPlannedProjectEndCommand> builder)
	{
		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.SetPlannedEnd.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var plannedEndUtc = builder.ClassProperty(r => r.PlannedEndUtc)
			.Required(Errors.Project.SetPlannedEnd.PlannedEndAtUtcMissing)
			.Map(p => p.PlannedEndUtc, value => new ProjectEndsAtUtc(value));

		return builder.Build(() => new SetPlannedProjectEndCommand(projectId, plannedEndUtc));
	}
}

public record SetPlannedProjectEndCommand(ProjectId ProjectId, ProjectEndsAtUtc PlannedEndUtc) : ICommand;
