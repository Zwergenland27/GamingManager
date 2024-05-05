using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.Finish;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Finish;

public class FinishProjectCommandBuilder : IRequestBuilder<FinishProjectParameters, FinishProjectCommand>
{
	public ValidatedRequiredProperty<FinishProjectCommand> Configure(RequiredPropertyBuilder<FinishProjectParameters, FinishProjectCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.Finish.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		return builder.Build(() => new FinishProjectCommand(auditorId, projectId));
	}
}

public record FinishProjectCommand(UserId AuditorId, ProjectId ProjectId) : ICommand;

