using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServerTickets.Commands.Accept;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.GameServerTickets.Commands.Accept;

public class AcceptGameServerTicketCommandBuilder : IRequestBuilder<AcceptTicketParameters, AcceptGameServerTicketCommand>
{
	public ValidatedRequiredProperty<AcceptGameServerTicketCommand> Configure(RequiredPropertyBuilder<AcceptTicketParameters, AcceptGameServerTicketCommand> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var id = builder.ClassProperty(r => r.Id)
			.Required(Errors.GameServerTicket.Accept.IdMissing)
			.Map(p => p.Id, GameServerTicketId.Create);

		var gameServerName = builder.ClassProperty(r => r.GameServerName)
			.Required(Errors.GameServerTicket.Accept.GameServerNameMissing)
			.Map(p => p.GameServerName, value => new GameServerName(value));

		return builder.Build(() => new AcceptGameServerTicketCommand(auditorId, id, gameServerName));
	}
}

public record AcceptGameServerTicketCommand(
	UserId AuditorId,
	GameServerTicketId Id,
	GameServerName GameServerName) : ICommand;
