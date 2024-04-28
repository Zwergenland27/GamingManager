using GamingManager.Contracts.Features.Users;

namespace GamingManager.Contracts.Features.Projects.Queries.Get;

public class GetProjectTeamMemberUserResult(
	string Id,
	string Username) : UserCoreResult(Id, Username)
{
}
