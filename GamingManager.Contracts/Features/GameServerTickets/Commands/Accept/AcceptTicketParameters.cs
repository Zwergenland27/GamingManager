using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServerTickets.Commands.Accept;

/// <summary>
/// Parameters for accepting a ticket
/// </summary>
public class AcceptTicketParameters : IParameters
{
	/// <summary>
	/// Id of the user that is accepting the ticket
	/// </summary>
	[JsonIgnore]
	public string? AuditorId { get; set; }

	/// <summary>
	/// Unique id of the ticket
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[JsonIgnore]
	public string? Id { get; init; }

	/// <summary>
	/// The name of the game server the ticket is accepted for
	/// </summary>
	/// <example>Minecraft-04</example>
	[Required]
	public string? GameServerName { get; init; }
}
