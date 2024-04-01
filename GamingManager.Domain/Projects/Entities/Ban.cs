using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Entities;

public class Ban : Entity<BanId>
{
	private Ban(
		ParticipantId participantId,
		Reason reason,
		DateTime bannedAtUtc,
		TimeSpan? duration) : base(BanId.CreateNew())
	{
		Participant = participantId;
		Reason = reason;
		BannedAtUtc = bannedAtUtc;
		Duration = duration;
	}

#pragma warning disable CS8618
	private Ban() : base(default!) { }
#pragma warning restore

	public ParticipantId Participant { get; private init; }

	public Reason Reason { get; private init; }

	public DateTime BannedAtUtc { get; private init; }

	public TimeSpan? Duration { get; private init; }

	public static Ban CreatePermanent(ParticipantId participantId, Reason reason, DateTime bannedAtUtc)
	{
		return new Ban(participantId, reason, bannedAtUtc, null);
	}

	public static Ban CreateTemporary(ParticipantId participantId, Reason reason, DateTime bannedAtUtc, TimeSpan duration)
	{
		return new Ban(participantId, reason, bannedAtUtc, duration);
	}
}
