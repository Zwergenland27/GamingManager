using GamingManager.Contracts.Features.Servers;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServers.Queries.Get;

public class GetGameServerServerResult(
		string Id,
		string HostName,
		string Status) : ServerCoreResult(Id, HostName, Status)
{
	/// <summary>
	/// Current status of the gameServer
	/// </summary>
	/// <example>Offline</example>
	[Required]
	public string Status { get; init; } = Status;
}
