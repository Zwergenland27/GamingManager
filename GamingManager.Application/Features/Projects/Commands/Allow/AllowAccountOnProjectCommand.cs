using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Commands.Allow;

public record AllowAccountOnProjectCommand(
	ProjectId ProjectId,
	AccountId AccountId) : ICommand;
