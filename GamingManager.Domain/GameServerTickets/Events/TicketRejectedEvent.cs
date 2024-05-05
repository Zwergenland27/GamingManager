using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.GameServerTickets.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.GameServerTickets.Events;

public record TicketRejectedEvent(GameServerTicketId TicketId, UserId ApplicantId, UserId IssuerId, TicketTitle Title, TicketDetails Reason) : IDomainEvent;
