using GamingManager.Application.Features.Games.DTOs;
using GamingManager.Domain.Accounts.ValueObjects;

namespace GamingManager.Application.Features.Accounts.DTOs;

public record ShortenedAccountDto(
	AccountId Id,
	AccountName Name,
	ShortenedGameDto Game)
{
}
