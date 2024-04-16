using GamingManager.Application.Features.Games.DTOs;
using GamingManager.Application.Features.Users.DTOs;
using GamingManager.Domain.Accounts.ValueObjects;

namespace GamingManager.Application.Features.Accounts.DTOs;

public record DetailedAccountDto(
	AccountId Id,
	AccountName Name,
	ShortenedUserDto? User,
	ShortenedGameDto Game,
	Uuid? Uuid,
	bool IsConfirmed)
{
}
