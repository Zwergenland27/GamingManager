using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Games.Queries.Get;

/// <summary>
/// Parameters for retrieving a game
/// </summary>
public record GetGameParameters : IParameters
{
    ///<summary>
    /// Name of the game
    /// </summary>
    /// <example>Minecraft</example>
    [Required]
    public string? Name { get; set; }
}