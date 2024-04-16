using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Ban;

public record BanAccountCommand(
	ProjectId ProjectId,
	AccountId AccountId,
	Reason Reason,
	TimeSpan? Duration) : ICommand;
