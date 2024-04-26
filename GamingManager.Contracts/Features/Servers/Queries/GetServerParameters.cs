using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Servers.Queries;

/// <summary>
/// Parameters for getting a server
/// </summary>
public class GetServerParameters : IParameters
{
	/// <summary>
	/// Hostname of the server
	/// </summary>
	///	<example>mineserv</example>
	[Required]
	public string? Hostname { get; set; }
}
