using GamingManager.Contracts.Features.Users;

namespace GamingManager.Contracts.Features.Projects.Commands.AddToTeam;

public class AddToTeamUserResult(
	string Id,
	string Username) : UserCoreResult(Id, Username)
{
}
