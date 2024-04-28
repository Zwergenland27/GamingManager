using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Entities;

public class Ban : Entity<BanId>
{
	private Ban(
		ProjectId projectId,
		ParticipantId participantId,
		Reason reason,
		DateTime bannedAtUtc,
		TimeSpan? duration) : base(BanId.CreateNew())
	{
		ParticipantId = participantId;
		Reason = reason;
		BannedAtUtc = bannedAtUtc;
		Duration = duration;
	}

#pragma warning disable CS8618
	private Ban() : base(default!) { }
#pragma warning restore

	public ProjectId ProjectId { get; private init; }

	public ParticipantId ParticipantId { get; private init; }

	public Reason Reason { get; private init; }

	public DateTime BannedAtUtc { get; private init; }

	public TimeSpan? Duration { get; private init; }

	public static Ban CreatePermanent(ProjectId projectId, ParticipantId participantId, Reason reason, DateTime bannedAtUtc)
	{
		return new Ban(projectId, participantId, reason, bannedAtUtc, null);
	}

	public static Ban CreateTemporary(ProjectId projectId, ParticipantId participantId, Reason reason, DateTime bannedAtUtc, TimeSpan duration)
	{
		return new Ban(projectId, participantId, reason, bannedAtUtc, duration);
	}
}
