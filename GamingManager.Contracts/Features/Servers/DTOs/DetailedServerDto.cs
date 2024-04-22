using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Servers.DTOs;

public record DetailedServerDto(
	string Id,
	string Hostname,
	string Mac,
	string Ip)
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
	/// Mac address of the server
	/// </summary>
	/// <example>AF:FE:DE:AD:DE:AD</example>
	[Required]
	public string Mac { get; init; } = Mac;

	/// <summary>
	/// IP address of the server
	/// </summary>
	/// <example>192.168.1.1</example>
	[Required]
	public string Ip { get; init; } = Ip;
}
