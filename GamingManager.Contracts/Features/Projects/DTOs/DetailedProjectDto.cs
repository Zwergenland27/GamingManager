using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Contracts.Features.GameServers.DTOs;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Application.Features.Projects.DTOs;

public record DetailedProjectDto(
	string Id,
	string Name,
	ShortenedGameDto Game,
	ShortenedGameServerForProjectDto? GameServer,
	DateTime StartsAtUtc,
	DateTime? EndsAtUtc,
	IEnumerable<DetailedMemberDto> Members,
	IEnumerable<DetailedParticipantDto> Participants)
{
	///<summary>
	/// Unique id of the project
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	/// <summary>
	/// Name of the project
	/// </summary>
	/// <example>SurvivalCraft</example>
	[Required]
	public string Name { get; init; } = Name;

	/// <summary>
	/// The start time of the project
	/// </summary>
	[Required]
	public DateTime StartsAtUtc { get; init; } = StartsAtUtc;

	/// <summary>
	/// The end time of the project
	/// </summary>
	public DateTime? EndsAtUtc { get; init; } = EndsAtUtc;

	/// <summary>
	/// The game that the project belongs to
	/// </summary>
	[Required]
	public ShortenedGameDto Game { get; init; } = Game;

	/// <summary>
	/// The game server on which the project runs
	/// </summary>
	public ShortenedGameServerForProjectDto? GameServer { get; init; } = GameServer;

	/// <summary>
	/// Team members of the project
	/// </summary>
	[Required]
	public IEnumerable<DetailedMemberDto> Members { get; init; } = Members;

	/// <summary>
	/// Participants of the project
	/// </summary>
	[Required]
	public IEnumerable<DetailedParticipantDto> Participants { get; init; } = Participants;
}
