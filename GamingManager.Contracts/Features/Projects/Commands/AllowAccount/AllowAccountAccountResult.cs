using GamingManager.Contracts.Features.Accounts;

namespace GamingManager.Contracts.Features.Projects.Commands.AllowAccount;

public class AllowAccountAccountResult(
	string Id,
	string Name,
	string? Uuid) : AccountCoreResult(Id, Name)
{
	///<summary>
	/// Uuid of the account
	/// </summary>
	/// <example>MinecraftUUID</example>
	public string? Uuid { get; init; } = Uuid;
}
