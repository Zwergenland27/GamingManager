using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Users.Queries;

/// <summary>
/// Parameters for querying a user.
/// </summary>
public class GetUserParameters : IParameters
{
	/// <summary>
	/// Current Username of the user
	/// </summary>
	/// <example>Zwergenland27</example>
	[JsonIgnore]
	public string? CurrentUsername { get; set; }
}
