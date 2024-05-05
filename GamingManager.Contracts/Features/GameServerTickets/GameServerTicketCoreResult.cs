using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServerTickets;

public class GameServerTicketCoreResult(
	string Id,
	string Title,
	string Details)
{
	/// <summary>
	/// Unique id of the ticket
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;


	/// <summary>
	/// Title of the ticket
	/// </summary>
	/// <example>Minecraft server request</example>
	[Required]
	public string Title { get; set; } = Title;

	/// <summary>
	/// Details for the ticket
	/// </summary>
	/// <remarks>Server with 1 to 10 players</remarks>
	[Required]
	public string Details { get; set; } = Details;
}
