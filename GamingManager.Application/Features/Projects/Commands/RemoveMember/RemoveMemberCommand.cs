using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.RemoveMember;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.RemoveMember;

public class RemoveMemberCommandBuilder : IRequestBuilder<RemoveMemberParameters, RemoveMemberCommand>
{
	public ValidatedRequiredProperty<RemoveMemberCommand> Configure(RequiredPropertyBuilder<RemoveMemberParameters, RemoveMemberCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.RemoveMember.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var memberId = builder.ClassProperty(r => r.MemberId)
			.Required(Errors.Project.RemoveMember.MemberIdMissing)
			.Map(u => u.MemberId, MemberId.Create);

		return builder.Build(() => new RemoveMemberCommand(auditorId, projectId, memberId));
	}
}

public record RemoveMemberCommand(
	UserId AuditorId,
	ProjectId ProjectId,
	MemberId MemberId) : ICommand;
