using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.Queries.GetAll;

public class GetAllProjectsGameResult(
	string Id,
	string Name,
	GetAllProjectsGameResult Game) : ProjectCoreResult(Id, Name)
{
	/// <summary>
	/// Game of the project
	/// </summary>
	[Required]
	public GetAllProjectsGameResult Game { get; init; } = Game;
}
