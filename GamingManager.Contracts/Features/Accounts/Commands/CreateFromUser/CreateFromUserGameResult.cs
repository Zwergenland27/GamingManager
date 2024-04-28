using GamingManager.Contracts.Features.Games;

namespace GamingManager.Contracts.Features.Accounts.Commands.CreateFromUser;

public class CreateFromUserGameResult(
	string Id,
	string Name) : GameCoreResult(Id, Name)
{
}
