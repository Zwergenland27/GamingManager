using GamingManager.Contracts.Features.Users;

namespace GamingManager.Contracts.Features.GameServerTickets.Queries.GetAllOpen;

public class GetAllOpenTicketsApplicantResult(
	string Id,
	string Username) : UserCoreResult(Id, Username)
{

}
