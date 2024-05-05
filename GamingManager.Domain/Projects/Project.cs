using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;
using GamingManager.Domain.Users;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Projects.Events;
using GamingManager.Domain.Projects.Entities;
using GamingManager.Domain.Users.ValueObjects;
using GamingManager.Domain.GameServerTickets.ValueObjects;
using GamingManager.Domain.GameServerRequests;
using GamingManager.Domain.Accounts.ValueObjects;

namespace GamingManager.Domain.Projects;

/// <summary>
/// A game project
/// </summary>
public class Project : AggregateRoot<ProjectId>
{
	private readonly List<Participant> _participants = [];

	private readonly List<Member> _members = [];

	private Project(
		ProjectId id,
		GameId gameId,
		ProjectName name,
		ProjectStartsAtUtc start) : base(id)
	{
		GameId = gameId;
		Name = name;
		Start = start;
		Public = false;
	}
#pragma warning disable CS8618
	private Project() : base(default!) { }
#pragma warning restore CS8618

	public GameId GameId { get; private set; }

	public GameServerId? ServerId { get; private set; }

	public ProjectName Name { get; private set; }

	public ProjectStartsAtUtc Start { get; private set; }

	public ProjectEndsAtUtc? End { get; private set; }

	public bool Ended
	{
		get
		{
			if(End is null) return false;
			return DateTime.UtcNow >= End.Value;
		}
	}

	public bool Public { get; private set; }

	public IReadOnlyCollection<Participant> Participants => _participants.AsReadOnly();

	public IReadOnlyCollection<Member> Members => _members.AsReadOnly();

	public bool PlayersOnline => _participants.Any(participant => participant.Online);

	public static CanFail<Project> Create(ProjectName name, Game game, ProjectStartsAtUtc start, User owner)
	{
		var project = new Project(ProjectId.CreateNew(), game.Id, name, start);
		var organisatorResult = project.AddMember(owner, MemberRole.Administrator);
		if (organisatorResult.HasFailed) return organisatorResult.Errors;
		project.RaiseDomainEvent(new ProjectCreatedEvent(project.Id));
		return project;
	}

	public CanFail<GameServerTicket> CreateTicket(UserId auditorUserId, TicketTitle title, TicketDetails details)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		if(ServerId is not null) return Errors.Projects.ServerAssigned;

		return GameServerTicket.Create(Id, auditorUserId, title, details);
	}

	public void SetServer(GameServerId serverId)
	{
		ServerId = serverId;
	}

	public CanFail Join(AccountId accountId, SessionStartsAtUtc joinTime)
	{
		var participant = _participants.FirstOrDefault(participant => participant.AccountId == accountId);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		if (Ended) return Errors.Projects.Ended;

		if (participant.IsCurrentlyBanned) return Errors.Projects.Participants.Banned;
		return participant.Join(ServerId!, joinTime);
	}

	public CanFail Leave(AccountId accountId, SessionEndsAtUtc leaveTime)
	{
		var participant = _participants.FirstOrDefault(participant => participant.AccountId == accountId);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		var lastPlayer = _participants.Count(participant => participant.Online) == 1;

		var leaveResult = participant.Leave(ServerId!, leaveTime, lastPlayer, false);
		if (leaveResult.HasFailed) return leaveResult.Errors;

		return CanFail.Success();
	}

	public CanFail Crashed(GameServerCrashedAtUtc crashedAtUtc)
	{
		var onlineParticipants = _participants.Where(participant => participant.Online);

		CanFail result = new();
		
		foreach (var participant in onlineParticipants)
		{
			var leaveResult = participant.Leave(ServerId!, new SessionEndsAtUtc(crashedAtUtc.Value), true);
			result.InheritFailure(result);
		}

		return result;
	}

	public CanFail<Ban> BanPermanent(UserId auditorUserId, ParticipantId participantId, Reason reason)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if(auditor is null) return Errors.Projects.Members.Forbidden;
		if(auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		var participant = _participants.FirstOrDefault(participant => participant.Id == participantId);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		return participant.BanPermanent(reason);
	}

	public CanFail<Ban> BanTemporary(UserId auditorUserId, ParticipantId participantId, Reason reason, TimeSpan duration)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		var participant = _participants.FirstOrDefault(participant => participant.Id == participantId);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		return participant.BanTemporary(reason, duration);
	}

	public CanFail Pardon(UserId auditorUserId, ParticipantId participantId)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		var participant = _participants.FirstOrDefault(participant => participant.Id == participantId);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		return participant.Pardon();
	}

	public CanFail<Participant> Allow(UserId auditorUserId, Account account)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		if (account.GameId != GameId) return Errors.Projects.Participants.WrongGame;
		if (!_members.Any(member => member.UserId == account.UserId)) return Errors.Projects.Members.NoMember;
		if (_participants.Any(participant => participant.AccountId == account.Id)) return Errors.Projects.Participants.AlreadyParticipating;

		var participant = new Participant(Id, account.Id);
		RaiseDomainEvent(new AccountAllowedEvent(Id, account.Id, participant.Id));
		return participant;
	}

	public CanFail AddMember(UserId auditorUserId, User user, MemberRole role)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		if(role == MemberRole.Administrator && auditor.Role != MemberRole.Administrator) return Errors.Projects.Members.Forbidden;

		return AddMember(user, role);
	}

	private CanFail AddMember(User user, MemberRole role)
	{
		if (_members.Any(teamMember => teamMember.UserId == user.Id)) return Errors.Projects.Members.AlreadyMember;

		var teamMember = new Member(Id, user.Id, role);

		_members.Add(teamMember);
		RaiseDomainEvent(new MemberAddedEvent(Id, user.Id, teamMember.Id, role));
		return CanFail.Success();
	}

	public CanFail ChangeMemberRole(UserId auditorUserId, MemberId memberId, MemberRole role)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		if (role == MemberRole.Administrator && auditor.Role != MemberRole.Administrator) return Errors.Projects.Members.Forbidden;

		var teamMember = _members.FirstOrDefault(teamMember => teamMember.Id == memberId);
		if (teamMember is null) return Errors.Projects.Members.NoMember;

		if (teamMember.Role == role) return CanFail.Success();
		if(teamMember.Role == MemberRole.Administrator && auditor.Role != MemberRole.Administrator) return Errors.Projects.Members.Forbidden;

		teamMember.Role = role;
		RaiseDomainEvent(new MemberRoleChangedEvent(Id, teamMember.Id, role));
		return CanFail.Success();
	}

	public CanFail RemoveMember(UserId auditorUserId, MemberId memberId)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		var teamMember = _members.FirstOrDefault(teamMember => teamMember.Id == memberId);
		if (teamMember is null) return Errors.Projects.Members.NoMember;

		if (!_members.Any(teamMember =>
			teamMember.Id != teamMember.Id &&
			teamMember.Role == MemberRole.Administrator)) return Errors.Projects.Members.NoAdmin;

		_members.Remove(teamMember);
		RaiseDomainEvent(new MemberRemovedEvent(Id, teamMember.Id));
		return CanFail.Success();
	}

	public bool CanStartServer(UserId requesterId) => _members.Any(member => member.UserId == requesterId);

	public CanFail RescheduleStart(UserId auditorUserId, ProjectStartsAtUtc startUtc)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		if (Ended) return Errors.Projects.Ended;
		if (startUtc.Value <= DateTime.UtcNow) return Errors.Projects.StartInPast;

		var oldStart = Start;
		Start = startUtc;
		RaiseDomainEvent(new ProjectStartRescheduledEvent(Id, oldStart, Start));
		return CanFail.Success();
	}

	public CanFail SetPlannedEnd(UserId auditorUserId, ProjectEndsAtUtc endUtc)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		if (Ended) return Errors.Projects.Ended;
		if (endUtc.Value <= DateTime.UtcNow) return Errors.Projects.EndInPast;
		if (endUtc.Value <= Start.Value) return Errors.Projects.EndBeforeStart;

		End = endUtc;
		RaiseDomainEvent(new ProjectEndPlannedEvent(Id, endUtc));
		return CanFail.Success();
	}

	public CanFail Finish(UserId auditorUserId)
	{
		var auditor = _members.FirstOrDefault(member => member.UserId == auditorUserId);
		if (auditor is null) return Errors.Projects.Members.Forbidden;
		if (auditor.Role != MemberRole.Administrator && auditor.Role != MemberRole.Moderator) return Errors.Projects.Members.Forbidden;

		End = new ProjectEndsAtUtc(DateTime.UtcNow);
		RaiseDomainEvent(new ProjectFinishedEvent(Id));
		return CanFail.Success();
	}
}
