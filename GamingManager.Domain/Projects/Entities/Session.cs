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
		ProjectId = projectId;
		ParticipantId = participantId;
		Start = start;
	}

#pragma warning disable CS8618
	private Session() : base(default!) { }
#pragma warning restore
	
	public ProjectId ProjectId { get; private init; }
	
	public ParticipantId ParticipantId { get; private init; }

	public SessionStartsAtUtc Start { get; private init; }

	public SessionEndsAtUtc? End { get; private set; }

	public TimeSpan Duration
	{
		get
		{
			DateTime endUtc = DateTime.UtcNow;
			if (End is not null)
			{
				endUtc = End.EndTime;
			}

			return endUtc.Subtract(Start.Value);
		}
	}

	internal CanFail Stop(SessionEndsAtUtc end, bool irregular = false)
	{
		if (end.EndTime < Start.Value) return Errors.Projects.Participants.Sessions.EndBeforeStart;
		End = new SessionEndsAtUtc(end.EndTime, irregular);
		return CanFail.Success();
	}
}
