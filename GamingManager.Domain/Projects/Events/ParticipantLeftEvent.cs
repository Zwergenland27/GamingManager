using GamingManager.Domain.Abstractions;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record ParticipantLeftEvent(ProjectId Project, ParticipantId Participant, GameServerId GameServer, SessionEndsAtUtc LeftAt, bool Irregular = false) : IDomainEvent;
