using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.Join;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Join;

public class JoinCommandBuilder : IRequestBuilder<JoinParameters, JoinCommand>
{
	public ValidatedRequiredProperty<JoinCommand> Configure(RequiredPropertyBuilder<JoinParameters, JoinCommand> builder)
	{
		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.Join.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var uuid = builder.ClassProperty(r => r.Uuid)
			.Required(Errors.Project.Join.UuidMissing)
			.Map(p => p.Uuid, value => new Uuid(value));

		var joinTimeUtc = builder.ClassProperty(r => r.JoinTimeUtc)
			.Required(Errors.Project.Join.JoinTimeUtcMissing)
			.Map(p => p.JoinTimeUtc, value => new SessionStartsAtUtc(value));

		return builder.Build(() => new JoinCommand(projectId, uuid, joinTimeUtc));
	}
}

public record JoinCommand(
	ProjectId ProjectId,
	Uuid Uuid,
	SessionStartsAtUtc JoinTimeUtc) : ICommand;
