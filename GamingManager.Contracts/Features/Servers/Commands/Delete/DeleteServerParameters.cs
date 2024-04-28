using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Servers.Commands.Delete;

/// <summary>
/// Parameters for deleting a server
/// </summary>
public class DeleteServerParameters : IParameters
{
    ///<summary>
    ///	Hostname of the server
    /// </summary>
    /// <example>mineserv</example>
    [JsonIgnore]
    public string? Hostname { get; set; }
}
