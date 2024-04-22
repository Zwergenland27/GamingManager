using GamingManager.Contracts.Features.Accounts.DTOs;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Application.Features.Projects.DTOs;

public record DetailedParticipantDto(
	string Id,
	ShortenedAccountDto Account,
	DateTime Since,
	bool Online,
	TimeSpan PlayTime)
{
	///<summary>
	/// Unique id of the participant
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	/// <summary>
	/// The account information of the participant
	/// </summary>
	[Required]
	public ShortenedAccountDto Account { get; init; } = Account;

	/// <summary>
	/// When the participant joined the project
	/// </summary>
	[Required]
	public DateTime Since { get; init; } = Since;

	/// <summary>
	/// Is the participant currently online
	/// </summary>
	[Required]
	public bool Online { get; init; } = Online;

	/// <summary>
	/// The total play time of the participant
	/// </summary>
	[Required]
	public TimeSpan PlayTime { get; init; } = PlayTime;
}
