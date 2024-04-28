using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Servers;

public class ServerCoreResult(
	string Id,
	string Hostname,
	string Status)
{
	///<summary>
	/// Unique id of the server
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	///<summary>
	///	Hostname of the server
	/// </summary>
	/// <example>mineserv</example>
	[Required]
	public string Hostname { get; init; } = Hostname;

	/// <summary>
	/// Status of the server
	/// </summary>
	/// <example>Offline</example>
	[Required]
	public string Status { get; init; } = Status;
}
