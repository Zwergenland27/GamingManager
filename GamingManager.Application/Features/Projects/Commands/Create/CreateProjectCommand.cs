using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.Create;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Create;

public class CreateProjectCommandBuilder : IRequestBuilder<CreateProjectParameters, CreateProjectCommand>
{
	public ValidatedRequiredProperty<CreateProjectCommand> Configure(RequiredPropertyBuilder<CreateProjectParameters, CreateProjectCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var gameName = builder.ClassProperty(r => r.GameName)
			.Required(Errors.Project.Create.GameNameMissing)
			.Map(p => p.GameName, value => new GameName(value));

		var projectName = builder.ClassProperty(r => r.ProjectName)
			.Required(Errors.Project.Create.ProjectNameMissing)
			.Map(p => p.ProjectName, value => new ProjectName(value));

		var projectStartsAtUtc = builder.ClassProperty(r => r.StartsAtUtc)
			.Required(Errors.Project.Create.StartsAtUtcMissing)
			.Map(p => p.StartsAtUtc, value => new ProjectStartsAtUtc(value));

		return builder.Build(() => new CreateProjectCommand(auditorId, gameName, projectName, projectStartsAtUtc));
	}
}

public record CreateProjectCommand(
	UserId AuditorId,
	GameName GameName,
	ProjectName ProjectName,
	ProjectStartsAtUtc StartsAtUtc) : ICommand<CreateProjectResult>;
