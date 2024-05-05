using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServerTickets.Queries.Get;

public class GetTicketResult(
	string Id,
	string Title,
	string Details,
	string? Annotation,
	string Status,
	GetTicketGameResult Game,
	GetTicketApplicantResult Applicant,
	GetTicketIssuerResult? Issuer) : GameServerTicketCoreResult(Id, Title, Details)
{

	/// <summary>
	/// Annotation by the issuer
	/// </summary>
	/// <example>Rejected because we do not support such huge amount of players</example>
	public string? Annotation { get; set; } = Annotation;

	/// <summary>
	/// The status of the ticket
	/// </summary>
	/// <example>Closed</example>
	[Required]
	public string Status { get; set; } = Status;

	/// <summary>
	/// The game that the ticket is for
	/// </summary>
	[Required]
	public GetTicketGameResult Game { get; set; } = Game;

	/// <summary>
	/// The user that opened the ticket
	/// </summary>
	[Required]
	public GetTicketApplicantResult Applicant { get; set; } = Applicant;

	/// <summary>
	/// The admin that closed the ticket
	/// </summary>
	public GetTicketIssuerResult? Issuer { get; set; } = Issuer;
}
