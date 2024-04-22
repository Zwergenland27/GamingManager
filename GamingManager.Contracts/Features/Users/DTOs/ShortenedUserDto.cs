using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Users.DTOs;

public record ShortenedUserDto(
	string Id,
	string Username)
{
	/// <summary>
	/// Unique id of the user
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	/// <summary>
	/// Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[Required]
	public string Username { get; init; } = Username;
}
