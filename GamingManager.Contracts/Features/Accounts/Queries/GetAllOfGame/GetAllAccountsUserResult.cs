using GamingManager.Contracts.Features.Users;

namespace GamingManager.Contracts.Features.Accounts.Queries.GetAllOfGame;

public class GetAllAccountsUserResult(
	string Id,
	string Username) : UserCoreResult(Id, Username)
{
}
