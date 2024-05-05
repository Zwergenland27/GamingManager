using GamingManager.Contracts.Features.Users;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.GameServerTickets.Queries.Get;

public class GetTicketApplicantResult(
	string Id,
	string Username,
	string? Firstname,
	string? Lastname,
	string Email) : UserCoreResult(Id, Username)
{
	/// <summary>
	/// First name of the user
	/// </summary>
	/// <example>Max</example>
	public string? Firstname { get; init; } = Firstname;

	/// <summary>
	/// Last name of the user
	/// </summary>
	/// <example>Mustermann</example>
	public string? Lastname { get; init; } = Lastname;

	/// <summary>
	/// Email of the user
	/// </summary>
	/// <example>zwergenland27@gmail.com</example>
	[Required]
	public string Email { get; init; } = Email;
}
