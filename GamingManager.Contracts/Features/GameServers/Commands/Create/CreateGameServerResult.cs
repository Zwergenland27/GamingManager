using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServers.Commands.Create;

public class CreateGameServerResult(
	string Id,
	string Name,
	string Status,
	uint ShutdownDelay,
	CreateGameServerProjectResult Project) : GameServerCoreResult(Id, Name, Status)
{
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
	public CreateGameServerProjectResult Project { get; init; } = Project;
}
