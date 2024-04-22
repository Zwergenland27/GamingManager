using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Commands.AssignUser;

public record AssignUuidCommand(
	GameName GameName,
	AccountName AccountName,
	Uuid Uuid) : ICommand<DetailedAccountDto>;
