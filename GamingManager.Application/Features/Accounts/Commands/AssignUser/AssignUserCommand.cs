using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Commands.AssignUser;

public record AssignUserCommand(
	GameName GameName,
	AccountName AccountName,
	Username Username) : ICommand<DetailedAccountDto>;
