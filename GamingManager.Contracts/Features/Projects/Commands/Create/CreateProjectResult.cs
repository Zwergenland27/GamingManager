using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.Commands.Create;

public class CreateProjectResult(
	string Id,
	string Name,
	CreateProjectGameResult Game,
	IReadOnlyCollection<CreateProjectTeamMemberResult> Team) : ProjectCoreResult(Id, Name)
{
	/// <summary>
	/// The game of the project
	/// </summary>
	[Required]
	public CreateProjectGameResult Game { get; init; } = Game;

	/// <summary>
	/// The team of the project
	/// </summary>
	[Required]
	public IReadOnlyCollection<CreateProjectTeamMemberResult> Team { get; init; } = Team;
}
