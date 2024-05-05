using GamingManager.Contracts.Features.Games;

namespace GamingManager.Contracts.Features.GameServerTickets.Queries.Get;

public class GetTicketGameResult(
	string Id,
	string Name) : GameCoreResult(Id, Name)
{

}
