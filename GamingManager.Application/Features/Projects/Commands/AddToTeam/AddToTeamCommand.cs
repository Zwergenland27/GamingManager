using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.AddToTeam;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.AddToTeam;

public class AddToTeamCommandBuilder : IRequestBuilder<AddToTeamParameters, AddToTeamCommand>
{
	public ValidatedRequiredProperty<AddToTeamCommand> Configure(RequiredPropertyBuilder<AddToTeamParameters, AddToTeamCommand> builder)
	{
		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.AddToTeam.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.Project.AddToTeam.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		var role = builder.EnumProperty(r => r.Role)
			.Required(Errors.Project.AddToTeam.RoleMissing)
			.Map(p => p.Role, Errors.Project.AddToTeam.RoleInvalid);

		return builder.Build(() => new AddToTeamCommand(projectId, username, role));
	}
}

public record AddToTeamCommand(
	ProjectId ProjectId,
	Username Username,
	TeamRole Role) : ICommand<AddToTeamResult>;
