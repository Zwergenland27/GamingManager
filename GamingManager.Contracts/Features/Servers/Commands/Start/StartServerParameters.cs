using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Servers.Commands.Start;

/// <summary>
/// Parameters for starting a server
/// </summary>
public class StartServerParameters : IParameters
{
	///<summary>
	///	Hostname of the server
	/// </summary>
	/// <example>mineserv</example>
	[JsonIgnore]
	public string? Hostname { get; set; }
}
