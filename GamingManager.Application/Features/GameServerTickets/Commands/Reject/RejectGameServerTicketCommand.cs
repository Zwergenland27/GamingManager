using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServerTickets.Commands.Reject;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.GameServerTickets.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.GameServerTickets.Commands.Reject;

public class RejectGameServerTicketCommandBuilder : IRequestBuilder<RejectTicketParameters, RejectGameServerTicketCommand>
{
	public ValidatedRequiredProperty<RejectGameServerTicketCommand> Configure(RequiredPropertyBuilder<RejectTicketParameters, RejectGameServerTicketCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var id = builder.ClassProperty(r => r.Id)
			.Required(Errors.GameServerTicket.Reject.IdMissing)
			.Map(p => p.Id, GameServerTicketId.Create);

		var reason = builder.ClassProperty(r => r.Reason)
			.Required(Errors.GameServerTicket.Reject.ReasonMissing)
			.Map(p => p.Reason, value => new TicketDetails(value));

		return builder.Build(() => new RejectGameServerTicketCommand(auditorId, id, reason));
	}
}

public record RejectGameServerTicketCommand(
	UserId AuditorId,
	GameServerTicketId Id,
	TicketDetails Reason) : ICommand;
