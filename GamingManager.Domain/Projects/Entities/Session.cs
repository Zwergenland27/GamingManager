using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Entities;

public class Session : Entity<SessionId>
{
	internal Session(
		ProjectId projectId,
		ParticipantId participantId,
		SessionStartsAtUtc start) : base(SessionId.CreateNew())
	{
		Project = projectId;
		Participant = participantId;
		Start = start;
	}

#pragma warning disable CS8618
	private Session() : base(default!) { }
#pragma warning restore

	public ProjectId Project { get; }

	public ParticipantId Participant { get; }

	public SessionStartsAtUtc Start { get; private init; }

	public SessionEndsAtUtc? End { get; private set; }

	public TimeSpan Duration
	{
		get
		{
			DateTime endUtc = DateTime.UtcNow;
			if (End is not null)
			{
				endUtc = End.Value;
			}

			return endUtc.Subtract(Start.Value);
		}
	}

	internal CanFail Stop(SessionEndsAtUtc end, bool irregular = false)
	{
		if (end.Value < Start.Value) return Errors.Projects.Participants.Sessions.EndBeforeStart;
		End = new SessionEndsAtUtc(end.Value, irregular);
		return CanFail.Success();
	}
}
