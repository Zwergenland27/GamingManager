using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Accounts.Queries.GetAllOfGame;

/// <summary>
/// Parameters for retrieving all accounts of a game
/// </summary>
public record GetAllAccountsParameters : IParameters
{
	///<summary>
	/// Name of the game
	/// </summary>
	/// <example>Minecraft</example>
	[Required]
	public string? GameName { get; set; }
}
