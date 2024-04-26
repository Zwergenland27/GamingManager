using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServers.Commands;

/// <summary>
/// Parameters for setting the server of a game server
/// </summary>
public class UseServerParameters : IParameters
{

	///<summary>
	/// Name of the gameserver
	/// </summary>
	/// <example>Minecraft-04</example>
	[JsonIgnore]
	public string? Name { get; set; }

	/// <summary>
	///	Hostname of the server
	/// </summary>
	/// <example>mineserv</example>
	[Required]
	public string? Hostname { get; set; }
}
