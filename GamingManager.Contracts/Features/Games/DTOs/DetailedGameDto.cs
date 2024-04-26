using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Contracts.Features.Projects.DTOs;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Games.DTOs;

public record DetailedGameDto(
	string Id,
	string Name,
	IReadOnlyCollection<ShortenedAccountDto> Accounts,
	IReadOnlyCollection<ShortenedProjectForGameDto> Projects)
{
	///<summary>
	/// Unique id of the game
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	///<summary>
	/// Name of the game
	/// </summary>
	/// <example>Minecraft</example>
	[Required]
	public string Name { get; init; } = Name;

	/// <summary>
	/// Accounts that belong to the game
	/// </summary>
	[Required]
	public IReadOnlyCollection<ShortenedAccountDto> Accounts { get; init; } = Accounts;

	/// <summary>
	/// Projects of the game
	/// </summary>
	[Required]
	public IReadOnlyCollection<ShortenedProjectForGameDto> Projects { get; init; } = Projects;
}
