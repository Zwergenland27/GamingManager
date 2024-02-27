﻿using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Domain.Projects.Events;

public record LastParticipantLeftEvent(ProjectId Project) : IDomainEvent;