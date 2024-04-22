using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Commands.ReAssignUser;

public record ReAssignUserCommand(
	GameName GameName,
	AccountName AccountName,
	Username Username) : ICommand<DetailedAccountDto>;
