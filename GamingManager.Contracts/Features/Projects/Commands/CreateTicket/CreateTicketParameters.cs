using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands.CreateTicket;

/// <summary>
/// Parameters for creating a ticket
/// </summary>
public class CreateTicketParameters : IParameters
{
	/// <summary>
	/// Id of the user that is adding the member
	/// </summary>
	[JsonIgnore]
	public string? AuditorId { get; set; }

	///<summary>
	/// Unique id of the project
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[JsonIgnore]
	public string? ProjectId { get; set; }

	/// <summary>
	/// Title of the ticket
	/// </summary>
	/// <example>Minecraft server request</example>
	[Required]
	public string? Title { get; set; }

	/// <summary>
	/// Details for the ticket
	/// </summary>
	/// <remarks>Server with 1 to 10 players</remarks>
	[Required]
	public string? Details { get; set; }
}
