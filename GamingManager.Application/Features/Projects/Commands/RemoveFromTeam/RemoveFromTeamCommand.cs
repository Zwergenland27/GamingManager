using GamingManager.Application.Abstractions;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.RemoveFromTeam;

public record RemoveFromTeamCommand(
	ProjectId ProjectId,
	Username Username) : ICommand;
