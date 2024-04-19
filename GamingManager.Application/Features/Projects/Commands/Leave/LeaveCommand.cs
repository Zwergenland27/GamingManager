using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Leave;

public record LeaveCommand(
	ProjectId ProjectId,
	Uuid Uuid,
	SessionEndsAtUtc LeaveTimeUtc) : ICommand;
