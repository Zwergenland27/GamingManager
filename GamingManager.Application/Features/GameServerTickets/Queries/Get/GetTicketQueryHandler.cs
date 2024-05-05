using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServerTickets.Queries.Get;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.GameServerTickets.Queries.Get;

public class GetTicketQueryHandler(
	IGameServerTicketDtoRepository gameServerTicketsDtoRepository) : IQueryHandler<GetTicketQuery, GetTicketResult>
{
	public async Task<CanFail<GetTicketResult>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
	{
		var ticket = await gameServerTicketsDtoRepository.GetAsync(request.Id, request.AuditorId);
		if (ticket is null) return Errors.GameServerTickets.Forbidden;

		return ticket;
	}
}
