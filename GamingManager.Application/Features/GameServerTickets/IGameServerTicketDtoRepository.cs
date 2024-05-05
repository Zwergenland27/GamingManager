using GamingManager.Contracts.Features.GameServerTickets.Queries.Get;
using GamingManager.Contracts.Features.GameServerTickets.Queries.GetAllOpen;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.GameServerTickets;

public interface IGameServerTicketDtoRepository
{
	Task<GetTicketResult?> GetAsync(GameServerTicketId id, UserId? applicantId);

	IAsyncEnumerable<GetAllOpenTicketsResult> GetAllOpenAsync();
}
