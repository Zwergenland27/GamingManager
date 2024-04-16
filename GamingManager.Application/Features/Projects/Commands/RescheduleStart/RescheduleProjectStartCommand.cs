using GamingManager.Application.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.RescheduleStart;

public record RescheduleProjectStartCommand(ProjectId ProjectId, ProjectStartsAtUtc ProjectStartsAtUtc) : ICommand;
