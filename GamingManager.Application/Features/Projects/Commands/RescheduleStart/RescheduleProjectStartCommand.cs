using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.RescheduleStart;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.RescheduleStart;

public class RescheduleProjectStartCommandBuilder : IRequestBuilder<RescheduleProjectStartParameters, RescheduleProjectStartCommand>
{
	public ValidatedRequiredProperty<RescheduleProjectStartCommand> Configure(RequiredPropertyBuilder<RescheduleProjectStartParameters, RescheduleProjectStartCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.Reschedule.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var plannedStartUtc = builder.ClassProperty(r => r.PlannedStartUtc)
			.Required(Errors.Project.Reschedule.PlannedStartUtcMissing)
			.Map(p => p.PlannedStartUtc, value => new ProjectStartsAtUtc(value));

		return builder.Build(() => new RescheduleProjectStartCommand(auditorId, projectId, plannedStartUtc));
	}
}

public record RescheduleProjectStartCommand(
	UserId AuditorId,
	ProjectId ProjectId,
	ProjectStartsAtUtc PlannedStartUtc) : ICommand;
