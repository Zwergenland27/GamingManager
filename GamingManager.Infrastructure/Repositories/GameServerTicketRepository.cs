using GamingManager.Domain.GameServerRequests;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.GameServerTickets;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class GameServerTicketRepository(GamingManagerDomainContext context) : IGameServerTicketRepository
{
	public void Add(GameServerTicket ticket)
	{
		context.Add(ticket);
	}

	public async Task<GameServerTicket?> GetAsync(GameServerTicketId id)
	{
		return await context.GameServerTickets.FirstOrDefaultAsync(ticket => ticket.Id == id);
	}
}
