using GamingManager.Contracts.Features.Users;

namespace GamingManager.Contracts.Features.Accounts.Queries.Get;

public class GetAccountUserResult(
	string Id,
	string Username) : UserCoreResult(Id, Username)
{
}
