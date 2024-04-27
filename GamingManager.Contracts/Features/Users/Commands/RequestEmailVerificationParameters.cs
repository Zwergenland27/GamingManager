using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Users.Commands;

/// <summary>
/// Parameters for requesting an email verification code
/// </summary>
public record RequestEmailVerificationParameters : IParameters
{
	/// <summary>
	/// Current Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[JsonIgnore]
	public string? Username { get; init; }
}
