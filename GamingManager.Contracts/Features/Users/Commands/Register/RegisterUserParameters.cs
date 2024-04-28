using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Users.Commands.Register;

/// <summary>
/// Create a new user account
/// </summary>
public record RegisterUserParameters : IParameters
{
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
    /// Username of the user
    /// </summary>
    /// <example>Zwergenland27</example>
    [Required]
    public string? Username { get; init; }

    /// <summary>
    /// Email of the user
    /// </summary>
    /// <example>user@gmail.com</example>
    [Required]
    public string? Email { get; init; }

    /// <summary>
    /// Password of the user
    /// </summary>
    /// <example>password1234</example>
    [Required]
    public string? Password { get; init; }
}
