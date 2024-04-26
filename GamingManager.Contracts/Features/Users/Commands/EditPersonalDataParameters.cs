using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Users.Commands;

/// <summary>
/// Edit personal data
/// </summary>
public record EditPersonalDataParameters : IParameters
{
	/// <summary>
	/// Current Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[JsonIgnore]
	public string? CurrentUsername { get; init; }

	/// <summary>
	/// First name of the user
	/// </summary>
	/// <example>Max</example>
	public string? Firstname { get; init; }

	/// <summary>
	/// Last name of the user
	/// </summary>
	/// <example>Mustermann</example>
	public string? Lastname { get; init; }

	/// <summary>
	/// New Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	public string? NewUsername { get; init; }

	/// <summary>
	/// Email of the user
	/// </summary>
	/// <example>user@gmail.com</example>
	public string? Email { get; init; }
}
