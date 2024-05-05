using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.GameServerTickets.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.GameServerTickets.Events;

public record TicketAcceptedEvent(GameServerTicketId TicketId, UserId ApplicantId, UserId IssuerId, TicketTitle Title, ProjectId ProjectId, GameServerId GameServerId) : IDomainEvent;
