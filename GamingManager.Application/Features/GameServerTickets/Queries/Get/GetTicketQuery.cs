using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServerTickets.Queries.Get;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.GameServerTickets.Queries.Get;

public class GetTicketQueryBuilder : IRequestBuilder<GetTicketParameters, GetTicketQuery>
{
	public ValidatedRequiredProperty<GetTicketQuery> Configure(RequiredPropertyBuilder<GetTicketParameters, GetTicketQuery> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Optional()
			.Map(p => p.AuditorId, UserId.Create);

		var id = builder.ClassProperty(r => r.Id)
			.Required(Errors.GameServerTicket.Get.IdMissing)
			.Map(p => p.Id, GameServerTicketId.Create);

		return builder.Build(() => new GetTicketQuery(auditorId, id));
	}
}

public record GetTicketQuery(
	UserId? AuditorId,
	GameServerTicketId Id) : IQuery<GetTicketResult>;
