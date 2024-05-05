using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.StartServer;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.StartServer;

public class StartProjectServerCommandBuilder : IRequestBuilder<StartProjectServerParameters, StartProjectServerCommand>
{
	public ValidatedRequiredProperty<StartProjectServerCommand> Configure(RequiredPropertyBuilder<StartProjectServerParameters, StartProjectServerCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.StartServer.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		return builder.Build(() => new StartProjectServerCommand(auditorId, projectId));
	}
}

public record StartProjectServerCommand(
	UserId AuditorId,
	ProjectId ProjectId) : ICommand;
