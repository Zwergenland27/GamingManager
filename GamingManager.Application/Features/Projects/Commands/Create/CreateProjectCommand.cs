using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Create;

public record CreateProjectCommand(
	GameName GameName,
	ProjectName projectName,
	ProjectStartsAtUtc ProjectStartsAtUtc,
	UserId UserId) : ICommand<DetailedProjectDto>;
