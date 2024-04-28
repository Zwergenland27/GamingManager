using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Servers.Commands.ChangeAddress;

/// <summary>
/// Parameters for changing the server address
/// </summary>
public class ChangeServerAddressParameters : IParameters
{
    ///<summary>
    ///	Hostname of the server
    /// </summary>
    /// <example>mineserv</example>
    [JsonIgnore]
    public string? Hostname { get; set; }

    /// <summary>
    /// IP address of the server
    /// </summary>
    /// <example>192.168.1.1</example>
    [Required]
    public string? Address { get; set; }
}
