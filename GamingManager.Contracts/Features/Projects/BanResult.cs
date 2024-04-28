using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Projects;

public class BanResult(
    string Id,
    string Reason,
    DateTime BannedAtUtc,
    TimeSpan? Duration)
{
    ///<summary>
    /// Unique id of the ban
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [Required]
    public string Id { get; init; } = Id;

    /// <summary>
    /// Reason of the ban
    /// </summary>
    /// <example>Griefing</example>
    [Required]
    public string Reason { get; init; } = Reason;

    /// <summary>
    /// The time when the ban was issued
    /// </summary>
    [Required]
    public DateTime BannedAtUtc { get; init; } = BannedAtUtc;

    /// <summary>
    /// Duration of the ban
    /// </summary>
    /// <remarks>
    /// If this field is null, the ban is permanent
    /// </remarks>
    public TimeSpan? Duration { get; init; } = Duration;
}
