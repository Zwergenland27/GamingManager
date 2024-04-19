using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.Events;
using GamingManager.Domain.Projects.ValueObjects;
using System.Net.Http.Headers;

namespace GamingManager.Domain.Projects.Entities;

public class Participant : Entity<ParticipantId>
{
    private readonly List<Session> _sessions = [];

    private readonly List<Ban> _bans = [];

    private readonly TimeSpan _playTime;
    internal Participant(
        ProjectId projectId,
        AccountId playerId) : base(ParticipantId.CreateNew())
    {
        Project = projectId;
		Account = playerId;
        Since = new ParticipatesSinceUtc(DateTime.UtcNow);
        Online = false;
        _playTime = new TimeSpan(0, 0, 0);
    }

#pragma warning disable CS8618
    private Participant() : base(default!) { }
#pragma warning restore

    public ProjectId Project { get; }

    public AccountId Account { get; }

    public ParticipatesSinceUtc Since { get; }

    public bool Online { get; private set; }

    public TimeSpan PlayTime => _playTime;

    public IReadOnlyCollection<Ban> Bans => _bans.AsReadOnly();

    public bool IsCurrentlyBanned
    {
        get
        {
			var lastBan = _bans
				.OrderBy(ban => ban.BannedAtUtc)
				.LastOrDefault();
            if (lastBan is null) return false;

            if(lastBan.Duration is null) return true;

            var banEndsAtUtc = lastBan.BannedAtUtc.Add(lastBan.Duration.Value);

            return banEndsAtUtc <= DateTime.UtcNow;
		}
    }

    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    internal CanFail<Ban> BanPermanent(Reason reason)
    {
        if(IsCurrentlyBanned) return Errors.Projects.Participants.AlreadyBanned;

        var ban = Ban.CreatePermanent(Project, Id, reason, DateTime.UtcNow);
        _bans.Add(ban);

        RaiseDomainEvent(new ParticipantBannedEvent(Project, Id, ban));
        return ban;
	}

	internal CanFail<Ban> BanTemporary(Reason reason, TimeSpan duration)
	{
		if (IsCurrentlyBanned) return Errors.Projects.Participants.AlreadyBanned;

		var ban = Ban.CreateTemporary(Project, Id, reason, DateTime.UtcNow, duration);
		_bans.Add(ban);

		RaiseDomainEvent(new ParticipantBannedEvent(Project, Id, ban));
		return ban;
	}

	internal CanFail Pardon()
    {
        if(!IsCurrentlyBanned) return Errors.Projects.Participants.NotBanned;

        var lastBan = _bans
			.OrderBy(ban => ban.BannedAtUtc)
			.Last();

        _bans.Remove(lastBan);

        RaiseDomainEvent(new ParticipantPardonnedEvent(Project, Id));
        return CanFail.Success();
    }

    internal CanFail Join(GameServerId gameServerId, SessionStartsAtUtc joinTime)
    {
        var lastSession = _sessions
            .OrderBy(session => session.Start)
            .LastOrDefault();
        if (lastSession is not null)
        {
            if (lastSession.End is null) return Errors.Projects.Participants.OpenSession;
            else if (joinTime.Value >= lastSession.End.EndTime) return Errors.Projects.Participants.JoinBeforePreviousSession;
		}

        var currentSession = new Session(Project, Id, joinTime);
        _sessions.Add(currentSession);
        Online = true;
        RaiseDomainEvent(new ParticipantJoinedEvent(Project, Id, gameServerId, joinTime));
        return CanFail.Success();

    }

    internal CanFail Leave(GameServerId gameServerId, SessionEndsAtUtc leaveTime, bool irregular = false)
    {
        var lastSession = _sessions
            .OrderBy(session => session.Start)
            .LastOrDefault();

        if (lastSession is null) return Errors.Projects.Participants.NoOpenSession;
        if(lastSession.End is not null && !lastSession.End.Irregular) return Errors.Projects.Participants.NoOpenSession;

		lastSession.Stop(leaveTime, irregular);
        Online = false;
        _playTime.Add(lastSession.Duration);
        RaiseDomainEvent(new ParticipantLeftEvent(Project, Id, gameServerId, leaveTime, irregular));
		return CanFail.Success();
	}
}
