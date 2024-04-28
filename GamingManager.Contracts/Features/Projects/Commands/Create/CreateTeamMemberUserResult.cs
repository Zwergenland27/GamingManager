using GamingManager.Contracts.Features.Users;

namespace GamingManager.Contracts.Features.Projects.Commands.Create;

public class CreateTeamMemberUserResult(
	string Id,
	string Username) : UserCoreResult(Id, Username)
{
}
