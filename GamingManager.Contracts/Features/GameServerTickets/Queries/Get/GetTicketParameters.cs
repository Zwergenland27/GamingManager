using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.GameServerTickets.Queries.Get;

/// <summary>
/// Parameters for getting a ticket
/// </summary>
public class GetTicketParameters : IParameters
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
	public string? Id { get; set; }
}
