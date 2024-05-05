using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.Leave;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Leave;

public class LeaveCommandBuilder : IRequestBuilder<LeaveParameters, LeaveCommand>
{
	public ValidatedRequiredProperty<LeaveCommand> Configure(RequiredPropertyBuilder<LeaveParameters, LeaveCommand> builder)
	{
		var gameServerId = builder.ClassProperty(r => r.GameServerId)
			.Required(Errors.General.GameServerIdMissing)
			.Map(p => p.GameServerId, GameServerId.Create);


		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.Leave.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var uuid = builder.ClassProperty(r => r.Uuid)
			.Required(Errors.Project.Leave.UuidMissing)
			.Map(p => p.Uuid, value => new Uuid(value));

		var leaveTimeUtc = builder.ClassProperty(r => r.LeaveTimeUtc)
			.Required(Errors.Project.Leave.LeaveTimeUtcMissing)
			.Map(p => p.LeaveTimeUtc, value => new SessionEndsAtUtc(value));

		return builder.Build(() => new LeaveCommand(gameServerId, projectId, uuid, leaveTimeUtc));
	}
}

public record LeaveCommand(
	GameServerId GameServerId,
	ProjectId ProjectId,
	Uuid Uuid,
	SessionEndsAtUtc LeaveTimeUtc) : ICommand;
