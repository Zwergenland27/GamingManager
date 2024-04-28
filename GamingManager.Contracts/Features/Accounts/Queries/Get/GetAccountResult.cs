using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Accounts.Queries.Get;

public class GetAccountResult(
	string Id,
	string Name,
	string? Uuid,
	GetAccountUserResult? User,
	bool Verified,
	IReadOnlyCollection<GetAccountProjectsResult> Projects) : AccountCoreResult(Id, Name)
{
	///<summary>
	/// Uuid of the account
	/// </summary>
	/// <example>MinecraftUUID</example>
	public string? Uuid { get; init; } = Uuid;

	/// <summary>
	/// The the user that owns the account
	/// </summary>
	public GetAccountUserResult? User { get; init; } = User;

	/// <summary>
	/// True, if the account was online at least once
	/// </summary>
	[Required]
	public bool Verified { get; init; } = Verified;

	/// <summary>
	/// Projects the account participates in
	/// </summary>
	[Required]
	public IReadOnlyCollection<GetAccountProjectsResult> Projects { get; init; } = Projects;
}
