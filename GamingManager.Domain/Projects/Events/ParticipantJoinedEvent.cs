using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record ParticipantJoinedEvent(ProjectId Project, ParticipantId Participant, GameServerId GameServer, SessionStartsAtUtc JoinetAt) : IDomainEvent;
