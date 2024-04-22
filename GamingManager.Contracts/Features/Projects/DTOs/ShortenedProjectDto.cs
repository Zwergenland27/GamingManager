using GamingManager.Contracts.Features.Games.DTOs;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Application.Features.Projects.DTOs;

public record ShortenedProjectDto(
	string Id,
	string Name,
	ShortenedGameDto Game)
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

	/// <summary>
	/// The game that the project belongs to
	/// </summary>
	[Required]
	public ShortenedGameDto Game { get; init; } = Game;
}
