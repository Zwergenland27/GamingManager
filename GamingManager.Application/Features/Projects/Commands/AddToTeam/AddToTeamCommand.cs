using GamingManager.Application.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.AddToTeam;

public record AddToTeamCommand(
	ProjectId ProjectId,
	Username Username,
	TeamRole Role) : ICommand;
