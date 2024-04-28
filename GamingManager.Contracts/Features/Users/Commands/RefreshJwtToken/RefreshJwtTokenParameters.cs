using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Users.Commands.RefreshJwtToken;

/// <summary>
/// Parameters for refreshing a jwt token
/// </summary>
public record RefreshJwtTokenParameters : IParameters
{
    /// <summary>
    /// Username of the user
    /// </summary>
    /// <example>Zwergenland27</example>
    [JsonIgnore]
    public string? Username { get; init; }

    /// <summary>
    /// Refresh token to refresh the jwt token
    /// </summary>
    [Required]
    public string? RefreshToken { get; init; }
}
