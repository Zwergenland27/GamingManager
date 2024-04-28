using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.RemoveFromTeam;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.RemoveFromTeam;

public class RemoveFromTeamCommandBuilder : IRequestBuilder<RemoveFromTeamParameters, RemoveFromTeamCommand>
{
	public ValidatedRequiredProperty<RemoveFromTeamCommand> Configure(RequiredPropertyBuilder<RemoveFromTeamParameters, RemoveFromTeamCommand> builder)
	{
		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.RemoveFromTeam.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.Project.RemoveFromTeam.UsernameMissing)
			.Map(u => u.Username, value => new Username(value));

		return builder.Build(() => new RemoveFromTeamCommand(projectId, username));
	}
}

public record RemoveFromTeamCommand(
	ProjectId ProjectId,
	Username Username) : ICommand;
