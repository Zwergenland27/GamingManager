using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Games.Commands;

/// <summary>
/// Parameters for creating a game
/// </summary>
public record CreateGameParameters : IParameters
{
	///<summary>
	/// Name of the game
	/// </summary>
	/// <example>Minecraft</example>
	[Required]
	public string? Name { get; set; }
}
