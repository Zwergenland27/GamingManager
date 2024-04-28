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
		var gameName = builder.ClassProperty(r => r.GameName)
			.Required(Errors.Project.Create.GameNameMissing)
			.Map(p => p.GameName, value => new GameName(value));

		var projectName = builder.ClassProperty(r => r.ProjectName)
			.Required(Errors.Project.Create.ProjectNameMissing)
			.Map(p => p.ProjectName, value => new ProjectName(value));

		var projectStartsAtUtc = builder.ClassProperty(r => r.StartsAtUtc)
			.Required(Errors.Project.Create.StartsAtUtcMissing)
			.Map(p => p.StartsAtUtc, value => new ProjectStartsAtUtc(value));

		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.Project.Create.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		return builder.Build(() => new CreateProjectCommand(gameName, projectName, projectStartsAtUtc, username));
	}
}

public record CreateProjectCommand(
	GameName GameName,
	ProjectName ProjectName,
	ProjectStartsAtUtc StartsAtUtc,
	Username Username) : ICommand<CreateProjectResult>;
