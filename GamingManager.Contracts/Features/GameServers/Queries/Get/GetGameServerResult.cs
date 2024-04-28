using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServers.Queries.Get;

public class GetGameServerResult(
	string Id,
	string Name,
	uint ShutdownDelay,
	string? Address,
	string Status,
	GetGameServerProjectResult Project,
	GetGameServerServerResult? HostedOn) : GameServerCoreResult(Id, Name, Status)
{
	/// <summary>
	/// Shutdown delay after last player left in minutes
	/// </summary>
	/// <example>15</example>
	[Required]
	public uint ShutdownDelay { get; init; } = ShutdownDelay;

	/// <summary>
	/// Domain of the game server
	/// </summary>
	/// <example>zwergenland.de</example>
	public string? Address { get; init; } = Address;

	/// <summary>
	/// Project the server belongs to
	/// </summary>
	[Required]
	public GetGameServerProjectResult Project { get; init; } = Project;

	/// <summary>
	/// The server the game server is hosted on
	/// </summary>
	public GetGameServerServerResult? HostedOn { get; init; } = HostedOn;
}
