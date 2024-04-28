using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Users.Commands.DeleteUser;

/// <summary>
/// Parameters for deleting a user
/// </summary>
public class DeleteUserParameters : IParameters
{
    /// <summary>
    /// Current Username of the user
    /// </summary>
    /// <example>Zwergenland27</example>
    [JsonIgnore]
    public string? Username { get; set; }
}
