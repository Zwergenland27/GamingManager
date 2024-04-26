using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Accounts.Queries;

public record GetAccountParameters : IParameters
{
	///<summary>
	/// Name of the game
	/// </summary>
	/// <example>Minecraft</example>
	[JsonIgnore]
	public string? GameName { get; init; }

	///<summary>
	/// Name of the account
	/// </summary>
	/// <example>Zwergenland</example>
	[JsonIgnore]
	public string? AccountName { get; init; }
}
