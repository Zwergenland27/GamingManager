using GamingManager.Application.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Finish;

public record FinishProjectCommand(ProjectId ProjectId) : ICommand;

