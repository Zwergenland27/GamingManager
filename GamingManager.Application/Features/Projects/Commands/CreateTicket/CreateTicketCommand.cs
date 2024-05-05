using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Commands.CreateTicket;
using GamingManager.Domain.GameServerTickets.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.CreateTicket;

public class CreateTicketCommandBuilder : IRequestBuilder<CreateTicketParameters, CreateTicketCommand>
{
	public ValidatedRequiredProperty<CreateTicketCommand> Configure(RequiredPropertyBuilder<CreateTicketParameters, CreateTicketCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.CreateTicket.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		var title = builder.ClassProperty(r => r.Title)
			.Required(Errors.Project.CreateTicket.TitleMissing)
			.Map(p => p.Title, value => new TicketTitle(value));

		var details = builder.ClassProperty(r => r.Details)
			.Required(Errors.Project.CreateTicket.DetailsMissing)
			.Map(p => p.Details, value => new TicketDetails(value));

		return builder.Build(() => new CreateTicketCommand(auditorId, projectId, title, details));
	}
}

public record CreateTicketCommand(
	UserId AuditorId,
	ProjectId ProjectId,
	TicketTitle Title,
	TicketDetails Details) : ICommand<CreateTicketResult>;
