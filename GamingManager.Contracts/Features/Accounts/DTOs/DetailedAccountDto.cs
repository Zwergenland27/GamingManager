using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Contracts.Features.Users.DTOs;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Accounts.DTOs;

public record DetailedAccountDto(
	string Id,
	string Name,
	string? Uuid,
	ShortenedUserDto? User,
	ShortenedGameDto Game,
	IReadOnlyCollection<ShortenedProjectDto> Projects)
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

	///<summary>
	/// Uuid of the account
	/// </summary>
	/// <example>MinecraftUUID</example>
	public string? Uuid { get; init; } = Uuid;

	/// <summary>
	/// The user that owns the account
	/// </summary>
	public ShortenedUserDto? User { get; init; } = User;

	/// <summary>
	/// The game that the account belongs to
	/// </summary>
	[Required]
	public ShortenedGameDto Game { get; init; } = Game;

	/// <summary>
	/// Projects the account participates in
	/// </summary>
	[Required]
	public IReadOnlyCollection<ShortenedProjectDto> Projects { get; init; } = Projects;
}
