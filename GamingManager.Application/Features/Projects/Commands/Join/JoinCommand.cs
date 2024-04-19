using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Join;

public record JoinCommand(
	ProjectId ProjectId,
	Uuid Uuid,
	SessionStartsAtUtc JoinTimeUtc) : ICommand;
