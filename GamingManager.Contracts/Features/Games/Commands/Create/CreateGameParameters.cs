using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Games.Commands.Create;

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

	/// <summary>
	/// True, if the player must authorize when creating its account
	/// </summary>
	/// <remarks>
	/// This should only be visible to admins
	/// </remarks>
	[Required]
    public bool? Verificationequired { get; set; }
}
