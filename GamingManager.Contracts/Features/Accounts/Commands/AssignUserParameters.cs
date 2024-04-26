using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Accounts.Commands;

public record AssignUserParameters: IParameters
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

	/// <summary>
	/// Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[Required]
	public string? Username { get; init; }
}
