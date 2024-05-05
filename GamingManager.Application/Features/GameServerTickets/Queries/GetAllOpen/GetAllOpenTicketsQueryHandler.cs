using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServerTickets.Queries.GetAllOpen;

namespace GamingManager.Application.Features.GameServerTickets.Queries.GetAllOpen;

public class GetAllOpenTicketsQueryHandler(
	IGameServerTicketDtoRepository gameServerTicketsDtoRepository) : IQueryHandler<GetAllOpenTickersQuery, IEnumerable<GetAllOpenTicketsResult>>
{
	public async Task<CanFail<IEnumerable<GetAllOpenTicketsResult>>> Handle(GetAllOpenTickersQuery request, CancellationToken cancellationToken)
	{
		return await gameServerTicketsDtoRepository.GetAllOpenAsync().ToListAsync();
	}
}
