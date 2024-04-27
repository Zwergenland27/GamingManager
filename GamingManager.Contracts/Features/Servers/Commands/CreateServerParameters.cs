using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Servers.Commands;

/// <summary>
/// Parameters for creating a new server
/// </summary>
public class CreateServerParameters : IParameters
{
	///<summary>
	///	Hostname of the server
	/// </summary>
	/// <example>mineserv</example>
	[Required]
	public string? Hostname { get; set; }

	/// <summary>
	/// IP address of the server
	/// </summary>
	/// <example>192.168.1.1</example>
	[Required]
	public string? Address { get; set; }

	/// <summary>
	/// Mac address of the server
	/// </summary>
	/// <example>AF:FE:DE:AD:DE:AD</example>
	[Required]
	public string? Mac { get; set; }

	/// <summary>
	/// Shutdown delay after last player left in minutes
	/// </summary>
	/// <example>15</example>
	[Required]
	public uint? ShutdownDelay { get; set; }
}
