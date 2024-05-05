using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServerTickets.Queries.GetAllOpen;

public class GetAllOpenTicketsResult(
	string Id,
	string Title,
	string Details,
	GetAllOpenTicketsGameResult Game,
	GetAllOpenTicketsApplicantResult Applicant) : GameServerTicketCoreResult(Id, Title, Details)
{
	/// <summary>
	/// The game that the ticket is for
	/// </summary>
	[Required]
	public GetAllOpenTicketsGameResult Game { get; set; } = Game;

	/// <summary>
	/// The user that opened the ticket
	/// </summary>
	[Required]
	public GetAllOpenTicketsApplicantResult Applicant { get; set; } = Applicant;
}
