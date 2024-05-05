namespace GamingManager.Contracts.Features.Games.Commands.Create;

public class CreateGameResult(
    string Id,
    string Name,
	bool VerificationRequired) : GameCoreResult(Id, Name)
{
	/// <summary>
	/// True, if the player must authorize when creating its account
	/// </summary>
	/// <remarks>
	/// This should only be visible to admins
	/// </remarks>
	public bool? VerificationRequired { get; init; } = VerificationRequired;
}
