using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;
using GamingManager.Domain.Users;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Projects.Events;
using GamingManager.Domain.Projects.Entities;

namespace GamingManager.Domain.Projects;

/// <summary>
/// A game project
/// </summary>
public class Project : AggregateRoot<ProjectId>
{
	private readonly List<Participant> _participants = [];

	private readonly List<TeamMember> _team = [];

	private Project(
		ProjectId id,
		GameId gameId,
		ProjectName name,
		ProjectStartsAtUtc start) : base(id)
	{
		Game = gameId;
		Name = name;
		Start = start;
		Ended = false;
	}
#pragma warning disable CS8618
	private Project() : base(default!) { }
#pragma warning restore CS8618

	public GameId Game { get; private set; }

	public GameServerId? Server { get; private set; }

	public ProjectName Name { get; private set; }

	public ProjectStartsAtUtc Start { get; private set; }

	public ProjectEndsAtUtc? End { get; private set; }

	public bool Ended { get; private set; }

	public IReadOnlyCollection<Participant> Participants => _participants.AsReadOnly();

	public IReadOnlyCollection<TeamMember> Team => _team.AsReadOnly();

	public bool PlayersOnline => _participants.Any(participant => participant.Online);

	public static CanFail<Project> Create(ProjectName name, Game game, ProjectStartsAtUtc start, User owner)
	{
		var project = new Project(ProjectId.CreateNew(), game.Id, name, start);
		var organisatorResult = project.AddToTeam(owner, TeamRole.Administrator);
		if (organisatorResult.HasFailed) return organisatorResult.Errors;
		project.RaiseDomainEvent(new ProjectCreatedEvent(project.Id));
		return project;
	}

	public CanFail Join(Account account, SessionStartsAtUtc joinTime)
	{
		var participant = _participants.FirstOrDefault(participant => participant.Account == account.Id);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		if (Ended) return Errors.Projects.Ended;

		if (participant.IsCurrentlyBanned) return Errors.Projects.Participants.Banned;
		return participant.Join(Server!, joinTime);
	}

	public CanFail Leave(Account account, SessionEndsAtUtc leaveTime, bool irregular = false)
	{
		var participant = _participants.FirstOrDefault(participant => participant.Account == account.Id);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		var leaveResult = participant.Leave(Server!, leaveTime, irregular);
		if (leaveResult.HasFailed) return leaveResult.Errors;

		if(!_participants.Any(participant => participant.Online))
		{
			RaiseDomainEvent(new LastParticipantLeftEvent(Id));
		}

		return CanFail.Success();
	}

	public CanFail<Ban> BanPermanent(Account account, Reason reason)
	{
		var participant = _participants.FirstOrDefault(participant => participant.Account == account.Id);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		return participant.BanPermanent(reason);
	}

	public CanFail<Ban> BanTemporary(Account account, Reason reason, TimeSpan duration)
	{
		var participant = _participants.FirstOrDefault(participant => participant.Account == account.Id);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		return participant.BanTemporary(reason, duration);
	}

	public CanFail Pardon(Account account)
	{
		var participant = _participants.FirstOrDefault(participant => participant.Account == account.Id);
		if (participant is null) return Errors.Projects.Participants.NotParticipating;

		return participant.Pardon();
	}

	public CanFail<Participant> Allow(Account account)
	{
		if (account.Game != Game) return Errors.Projects.Participants.WrongGame;
		if (_participants.Any(participant => participant.Account == account.Id)) return Errors.Projects.Participants.AlreadyParticipating;

		var participant = new Participant(Id, account.Id);
		RaiseDomainEvent(new AccountAllowedEvent(Id, account.Id, participant.Id));
		return participant;
	}

	public CanFail AddToTeam(User user, TeamRole role)
	{
		if (_team.Any(teamMember => teamMember.User == user.Id)) return Errors.Projects.Team.AlreadyMember;

		var teamMember = new TeamMember(Id, user.Id, role);

		_team.Add(teamMember);
		RaiseDomainEvent(new TeamMemberAddedEvent(Id, user.Id, teamMember.Id, role));
		return CanFail.Success();
	}

	public CanFail RemoveFromTeam(User user)
	{
		var teamMember = _team.FirstOrDefault(teamMember => teamMember.User == user.Id);
		if (teamMember is null) return Errors.Projects.Team.NoMember;

		if (!_team.Any(teamMember =>
			teamMember.Id != teamMember.Id &&
			teamMember.Role == TeamRole.Administrator)) return Errors.Projects.Team.NoAdmin;

		_team.Remove(teamMember);
		RaiseDomainEvent(new TeamMemberRemovedEvent(Id, teamMember.Id));
		return CanFail.Success();
	}

	public CanFail RescheduleStart(ProjectStartsAtUtc startUtc)
	{
		if (Ended) return Errors.Projects.Ended;
		if (startUtc.Value <= DateTime.UtcNow) return Errors.Projects.StartInPast;

		var oldStart = Start;
		Start = startUtc;
		RaiseDomainEvent(new ProjectStartRescheduledEvent(Id, oldStart, Start));
		return CanFail.Success();
	}

	public CanFail SetPlannedEnd(ProjectEndsAtUtc endUtc)
	{
		if (Ended) return Errors.Projects.Ended;
		if (endUtc.Value <= DateTime.UtcNow) return Errors.Projects.EndInPast;
		if (endUtc.Value <= Start.Value) return Errors.Projects.EndBeforeStart;

		End = endUtc;
		RaiseDomainEvent(new ProjectEndPlannedEvent(Id, endUtc));
		return CanFail.Success();
	}

	public void Finish()
	{
		Ended = true;
		End = new ProjectEndsAtUtc(DateTime.UtcNow);
		RaiseDomainEvent(new ProjectFinishedEvent(Id));
	}
}
