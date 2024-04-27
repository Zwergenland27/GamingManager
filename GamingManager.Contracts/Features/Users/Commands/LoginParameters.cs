using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Users.Commands;

/// <summary>
/// Parameters for logging in a user
/// </summary>
public record LoginParameters : IParameters
{
	/// <summary>
	/// Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	public string? Username { get; init; }

	/// <summary>
	/// Email of the user
	/// </summary>
	/// <example>user@gmail.com</example>
	public string? Email { get; init; }

	/// <summary>
	/// Password of the user
	/// </summary>
	/// <example>password1234</example>
	[Required]
	public string? Password { get; init; }
}
