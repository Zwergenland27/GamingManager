namespace GamingManager.Contracts.Features.Accounts.Queries.GetAllOfGame;

public class GetAllAccountsResult(
	string Id,
	string Name,
	string? Uuid,
	GetAllAccountsUserResult? User) : AccountCoreResult(Id, Name)
{
	///<summary>
	/// Uuid of the account
	/// </summary>
	/// <example>MinecraftUUID</example>
	public string? Uuid { get; init; } = Uuid;

	/// <summary>
	/// The user that owns the account
	/// </summary>
	public GetAllAccountsUserResult? User { get; init; } = User;

}
