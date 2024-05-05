using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.GameServerTickets.Events;
using GamingManager.Domain.GameServerTickets.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.GameServerRequests;

public class GameServerTicket : AggregateRoot<GameServerTicketId>
{
	public GameServerTicket(
		GameServerTicketId id,
		ProjectId projectId,
		UserId applicantId,
		TicketTitle title,
		TicketDetails details) : base(id)
	{
		ProjectId = projectId;
		ApplicantId = applicantId;
		Title = title;
		Details = details;
		Status = TicketStatus.Open;
	}
	public ProjectId ProjectId { get; private set; }

	public UserId ApplicantId { get; private set; }
	public TicketTitle Title { get; private set; }

	public TicketDetails Details { get; private set; }


	public TicketDetails? Annotation { get; private set; }

	public TicketStatus Status { get; private set; }

	internal static GameServerTicket Create(ProjectId projectId, UserId applicantId, TicketTitle title, TicketDetails details)
	{
		var ticket = new GameServerTicket(GameServerTicketId.CreateNew(), projectId, applicantId, title, details);

		ticket.RaiseDomainEvent(new TicketCreatedEvent(ticket.Id, title, details));
		
		return ticket;
	}

	public CanFail Reject(UserId issuerId, TicketDetails reason)
	{
		if (Status != TicketStatus.Open) return Errors.GameServerTickets.AlreadyClosed;

		Status = TicketStatus.Rejected;
		Annotation = reason;

		RaiseDomainEvent(new TicketRejectedEvent(Id, ApplicantId, issuerId, Title, reason));

		return CanFail.Success();
	}

	public CanFail Accept(UserId issuerId, GameServerId gameServerId)
	{
		if (Status != TicketStatus.Open) return Errors.GameServerTickets.AlreadyClosed;

		Status = TicketStatus.Accepted;

		RaiseDomainEvent(new TicketAcceptedEvent(Id, ApplicantId, issuerId, Title, ProjectId, gameServerId));

		return CanFail.Success();
	}
}
