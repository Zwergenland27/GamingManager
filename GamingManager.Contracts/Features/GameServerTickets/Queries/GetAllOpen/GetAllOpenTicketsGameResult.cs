using GamingManager.Contracts.Features.Games;

namespace GamingManager.Contracts.Features.GameServerTickets.Queries.GetAllOpen;

public class GetAllOpenTicketsGameResult(
	string Id,
	string Name) : GameCoreResult(Id, Name)
{

}
