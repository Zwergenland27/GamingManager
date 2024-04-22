using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Servers.DTOs;

public record ShortenedServerDto(
	string Id,
	string Hostname)
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
}
