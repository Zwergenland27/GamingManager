using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Users.Commands;

/// <summary>
/// Parameters for verifying the email of a user
/// </summary>
public class VerifyEmailParameters : IParameters
{
	/// <summary>
	/// Current Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[JsonIgnore]
	public string? Username { get; init; }

	/// <summary>
	/// The verification token
	/// </summary>
	[Required]
	public string? Token { get; init;}
}
