using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.DTOs;

public record ShortenedProjectForGameDto(
    string Id,
    string Name)
{
    ///<summary>
    /// Unique id of the project
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [Required]
    public string Id { get; init; } = Id;

    /// <summary>
    /// Name of the project
    /// </summary>
    /// <example>SurvivalCraft</example>
    [Required]
    public string Name { get; init; } = Name;
}
