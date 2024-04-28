using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Servers.Commands.ReceiveHeartbeat;

/// <summary>
/// Parameters for receiving a heartbeat
/// </summary>
public class ReceiveHeartbeatParameters : IParameters
{
    ///<summary>
    ///	Hostname of the server
    /// </summary>
    /// <example>mineserv</example>
    [JsonIgnore]
    public string? Hostname { get; set; }
}
