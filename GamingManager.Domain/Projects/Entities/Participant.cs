using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects.Events;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Entities;

public class Participant : Entity<ParticipantId>
{
    private readonly List<Session> _sessions = [];
    internal Participant(
        ProjectId projectId,
        AccountId playerId) : base(ParticipantId.CreateNew())
    {
        Project = projectId;
		Account = playerId;
        Since = new ParticipatesSinceUtc(DateTime.UtcNow);
        Online = false;
    }

#pragma warning disable CS8618
    private Participant() : base(default!) { }
#pragma warning restore

    public ProjectId Project { get; }

    public AccountId Account { get; }

    public ParticipatesSinceUtc Since { get; }

    public Reason? BanReason { get; private set; }

    public bool Online { get; private set; }

    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    internal CanFail Ban(Reason reason)
    {
        if (BanReason is not null) return Errors.Projects.Participants.AlreadyBanned;
        BanReason = reason;
        RaiseDomainEvent(new ParticipantBannedEvent(Project, Id, reason));
        return CanFail.Success();
    }

    internal CanFail Pardon()
    {
        if (BanReason is null) return Errors.Projects.Participants.NotBanned;
        BanReason = null;
        RaiseDomainEvent(new ParticipantPardonnedEvent(Project, Id));
        return CanFail.Success();
    }

    internal CanFail Join(SessionStartsAtUtc joinTime)
    {
        var lastSession = _sessions
            .OrderBy(session => session.Start)
            .LastOrDefault();
        if (lastSession is not null)
        {
            if (lastSession.End is null) return Errors.Projects.Participants.OpenSession;
            else if (joinTime.Value >= lastSession.End.Value) return Errors.Projects.Participants.JoinBeforePreviousSession;
		}

        var currentSession = new Session(Project, Id, joinTime);
        _sessions.Add(currentSession);
        Online = true;
        RaiseDomainEvent(new ParticipantJoinedEvent(Project, Id, joinTime));
        return CanFail.Success();

    }

    internal CanFail Leave(SessionEndsAtUtc leaveTime, bool irregular = false)
    {
        var lastSession = _sessions
            .OrderBy(session => session.Start)
            .LastOrDefault();

        if (lastSession is null) return Errors.Projects.Participants.NoOpenSession;
        if(lastSession.End is not null && !lastSession.End.Irregular) return Errors.Projects.Participants.NoOpenSession;

		lastSession.Stop(leaveTime, irregular);
        Online = false;
        RaiseDomainEvent(new ParticipantLeftEvent(Project, Id, leaveTime, irregular));
		return CanFail.Success();
	}
}
