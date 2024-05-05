using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.ChangeMemberRole;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.ChangeMemberRole;

public class ChangeMemberRoleCommandBuilder : IRequestBuilder<ChangeMemberRoleParameters, ChangeMemberRoleCommand>
{
	public ValidatedRequiredProperty<ChangeMemberRoleCommand> Configure(RequiredPropertyBuilder<ChangeMemberRoleParameters, ChangeMemberRoleCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.ChangeMemberRole.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var username = builder.ClassProperty(r => r.MemberId)
			.Required(Errors.Project.ChangeMemberRole.MemberIdMissing)
			.Map(p => p.MemberId, MemberId.Create);

		var role = builder.EnumProperty(r => r.NewRole)
			.Required(Errors.Project.ChangeMemberRole.NewRoleMissing)
			.Map(p => p.NewRole, Errors.Project.ChangeMemberRole.NewRoleInvalid);

		return builder.Build(() => new ChangeMemberRoleCommand(auditorId, projectId, username, role));
	}
}

public record ChangeMemberRoleCommand(
	UserId AuditorId,
	ProjectId ProjectId,
	MemberId MemberId,
	MemberRole NewRole) : ICommand;
