using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServerTickets.Commands.Reject;

/// <summary>
/// Parameters for rejecting a ticket
/// </summary>
public class RejectTicketParameters : IParameters
{
	/// <summary>
	/// Id of the user that is rejecting the ticket
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
    /// The reason the ticket was rejected
    /// </summary>
    /// <example>We cant handle so many users on our servers</example>
    [Required]
    public string? Reason { get; init; }
}
