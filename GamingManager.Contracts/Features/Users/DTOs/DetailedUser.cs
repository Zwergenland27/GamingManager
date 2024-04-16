using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Users.DTOs;

/// <summary>
/// Get all details of a user
/// </summary>
public record DetailedUser(
	string Id,
	string? Firstname,
	string? Lastname,
	string Username,
	string Email,
	string Role)
{

	/// <summary>
	/// Unique id of the user
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	/// <summary>
	/// First name of the user
	/// </summary>
	/// <example>Max</example>
	public string? Firstname { get; init; } = Firstname;

	/// <summary>
	/// Last name of the user
	/// </summary>
	/// <example>Mustermann</example>
	public string? Lastname { get; init; } = Lastname;

	/// <summary>
	/// Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[Required]
	public string Username { get; init; } = Username;

	/// <summary>
	/// Email of the user
	/// </summary>
	/// <example>user@gmail.com</example>
	[Required]
	public string Email { get; init; } = Email;

	/// <summary>
	/// The role of the user
	/// </summary>
	/// <example>Guest</example>
	[Required]
	public string Role { get; init; } = Role;
}
