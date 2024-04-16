using GamingManager.Application.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.SetPlannedEnd;

public record SetPlannedProjectEndCommand(ProjectId ProjectId, ProjectEndsAtUtc PlannedEntUtc) : ICommand;
