using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Users.Commands;

/// <summary>
/// Parameters for changing the password of a user
/// </summary>
public class ChangePasswordParameters : IParameters
{
	/// <summary>
	/// Current Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[JsonIgnore]
	public string? Username { get; set; }

	/// <summary>
	/// New password for the user
	/// </summary>
	/// <example>Sicher</example>
	[Required]
	public string? Password { get; set; }
}
