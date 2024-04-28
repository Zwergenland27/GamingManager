using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Users.Commands.RequestPasswordReset;
/// <summary>
/// Parameters for requesting a password reset
/// </summary>
public record RequestPasswordResetParameters : IParameters
{
    /// <summary>
    /// Current email of the user
    /// </summary>
    /// <example>user@gmail.com</example>
    [JsonIgnore]
    public string? Email { get; init; }
}