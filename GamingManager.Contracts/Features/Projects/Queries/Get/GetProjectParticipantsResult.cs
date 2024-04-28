using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.Queries.Get;

public class GetProjectParticipantsResult(
	string Id,
	GetProjectParticipantAccountResult Account,
	DateTime SinceUtc,
	bool Online,
	TimeSpan PlayTime,
	IReadOnlyCollection<BanResult>? Bans)
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
	public GetProjectParticipantAccountResult Account { get; init; } = Account;

	/// <summary>
	/// When the participant joined the project
	/// </summary>
	[Required]
	public DateTime Since { get; init; } = SinceUtc;

	/// <summary>
	/// Is the participant online
	/// </summary>
	[Required]
	public bool Online { get; init; } = Online;

	/// <summary>
	/// The total play time of the participant
	/// </summary>
	[Required]
	public TimeSpan PlayTime { get; init; } = PlayTime;

	/// <summary>
	/// Bans of the participant
	/// </summary>
	/// <remarks>
	/// This should only be visible to project team members
	/// </remarks>
	public IReadOnlyCollection<BanResult>? Bans { get; init; } = Bans;
}
