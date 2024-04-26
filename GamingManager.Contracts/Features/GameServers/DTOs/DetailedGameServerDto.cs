using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Contracts.Features.Servers.DTOs;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServers.DTOs;

public record DetailedGameServerDto(
	string Id,
	string Name,
	uint ShutdownDelay,
	ShortenedProjectDto Project,
	DetailedServerDto? HostedOn)
{
	/// <summary>
	/// Unique id of the server
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	/// <summary>
	/// Name of the gameserver
	/// </summary>
	/// <example>Minecraft-04</example>
	[Required]
	public string Name { get; init; } = Name;

	/// <summary>
	/// Shutdown delay after last player left in minutes
	/// </summary>
	/// <example>15</example>
	[Required]
	public uint ShutdownDelay { get; init; } = ShutdownDelay;

	/// <summary>
	/// The project the server belongs to
	/// </summary>
	[Required]
	public ShortenedProjectDto Project { get; init; } = Project;

	/// <summary>
	/// The hardware server the gameserver is hosted on
	/// </summary>
	/// <remarks>
	/// Only admins and team members can see this information
	/// </remarks>
	public DetailedServerDto? HostedOn { get; init; } = HostedOn;
}
