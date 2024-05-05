using GamingManager.Contracts.Features.GameServerTickets;

namespace GamingManager.Contracts.Features.Projects.Commands.CreateTicket;

public class CreateTicketResult(
	string Id,
	string Title,
	string Details) : GameServerTicketCoreResult(Id, Title, Details);
