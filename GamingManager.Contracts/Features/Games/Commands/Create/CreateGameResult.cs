namespace GamingManager.Contracts.Features.Games.Commands.Create;

public class CreateGameResult(
    string Id,
    string Name) : GameCoreResult(Id, Name)
{
}
