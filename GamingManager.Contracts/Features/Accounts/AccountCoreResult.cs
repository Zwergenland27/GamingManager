using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Accounts;

public class AccountCoreResult(
	string Id,
	string Name)
{
	///<summary>
	/// Unique id of the account
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	///<summary>
	/// Name of the account
	/// </summary>
	/// <example>Zwergenland</example>
	[Required]
	public string Name { get; init; } = Name;
}
