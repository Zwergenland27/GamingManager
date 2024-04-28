using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.Finish;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Finish;

public class FinishProjectCommandBuilder : IRequestBuilder<FinishProjectParameters, FinishProjectCommand>
{
	public ValidatedRequiredProperty<FinishProjectCommand> Configure(RequiredPropertyBuilder<FinishProjectParameters, FinishProjectCommand> builder)
	{
		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.Finish.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		return builder.Build(() => new FinishProjectCommand(projectId));
	}
}

public record FinishProjectCommand(ProjectId ProjectId) : ICommand;

