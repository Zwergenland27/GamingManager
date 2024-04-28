using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.Commands.AllowAccount;

public class AllowAccountResult(
	string Id,
	AllowAccountAccountResult Account,
	DateTime SinceUtc)
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
	public AllowAccountAccountResult Account { get; init; } = Account;

	/// <summary>
	/// When the participant joined the project
	/// </summary>
	[Required]
	public DateTime Since { get; init; } = SinceUtc;
}
