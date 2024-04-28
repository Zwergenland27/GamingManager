using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.Queries.Get;

public class GetProjectResult(
	string Id,
	string Name,
	GetProjectGameResult Game,
	GetProjectGameServerResult? Server,
	IReadOnlyCollection<GetProjectParticipantsResult> Participants,
	IReadOnlyCollection<GetProjectTeamMemberResult> Team) : ProjectCoreResult(Id, Name)
{
	/// <summary>
	/// The game of the project
	/// </summary>
	[Required]
	public GetProjectGameResult Game { get; init; } = Game;

	/// <summary>
	/// The server on which the project is hosted
	/// </summary>
	public GetProjectGameServerResult? Server { get; init; } = Server;

	/// <summary>
	/// Participants of the project
	/// </summary>
	[Required]
	public IReadOnlyCollection<GetProjectParticipantsResult> Participants { get; init; } = Participants;

	/// <summary>
	/// The team of the project
	/// </summary>
	[Required]
	public IReadOnlyCollection<GetProjectTeamMemberResult> Team { get; init; } = Team;
}
