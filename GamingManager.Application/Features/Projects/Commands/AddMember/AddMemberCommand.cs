using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.AddToTeam;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.AddMember;

public class AddMemberCommandBuilder : IRequestBuilder<AddMemberParameters, AddMemberCommand>
{
	public ValidatedRequiredProperty<AddMemberCommand> Configure(RequiredPropertyBuilder<AddMemberParameters, AddMemberCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.AddMember.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.Project.AddMember.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		var role = builder.EnumProperty(r => r.Role)
			.Required(Errors.Project.AddMember.RoleMissing)
			.Map(p => p.Role, Errors.Project.AddMember.RoleInvalid);

		return builder.Build(() => new AddMemberCommand(auditorId, projectId, username, role));
	}
}

public record AddMemberCommand(
	UserId AuditorId,
	ProjectId ProjectId,
	Username Username,
	MemberRole Role) : ICommand<AddMemberResult>;
