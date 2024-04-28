using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Servers.Commands.ChangeHostname;

/// <summary>
/// Parameters for changing the hostname
/// </summary>
public class ChangeHostnameParameters : IParameters
{
    ///<summary>
    ///	Hostname of the server
    /// </summary>
    /// <example>mineserv</example>
    [JsonIgnore]
    public string? CurrentHostname { get; set; }

    ///<summary>
    ///	Hostname of the server
    /// </summary>
    /// <example>mineserv-02</example>
    [Required]
    public string? NewHostname { get; set; }
}
