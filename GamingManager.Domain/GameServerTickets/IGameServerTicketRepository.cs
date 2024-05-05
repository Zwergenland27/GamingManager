using GamingManager.Domain.GameServerRequests;
using GamingManager.Domain.GameServerRequests.ValueObjects;

namespace GamingManager.Domain.GameServerTickets;

public interface IGameServerTicketRepository
{
	void Add(GameServerTicket ticket);

	Task<GameServerTicket?> GetAsync(GameServerTicketId id);
}
