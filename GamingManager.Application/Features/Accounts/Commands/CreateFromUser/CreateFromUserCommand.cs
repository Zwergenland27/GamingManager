using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Commands.CreateFromLogin;

public record CreateFromUserCommand(
	GameName GameName,
	AccountName AccountName,
	Username Username) : ICommand<DetailedAccountDto>;
