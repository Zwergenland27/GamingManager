using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects.DTOs;

/// <summary>
/// Information about a ban
/// </summary>
public record BanDto(
	string Reason,
	TimeSpan? Duration)
{
	/// <summary>
	/// Reason of the ban
	/// </summary>
	/// <example>Griefing</example>
	[Required]
	public string Reason { get; init; } = Reason;

	/// <summary>
	/// Duration of the ban
	/// </summary>
	/// <remarks>
	/// If this field is null, the ban is permanent
	/// </remarks>
	public TimeSpan? Duration { get; init; } = Duration;
}
