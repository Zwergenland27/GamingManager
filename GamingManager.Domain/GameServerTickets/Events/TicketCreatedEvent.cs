using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.GameServerTickets.ValueObjects;

namespace GamingManager.Domain.GameServerTickets.Events;

public record TicketCreatedEvent(GameServerTicketId TicketId, TicketTitle Title, TicketDetails Details) : IDomainEvent;
