using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.DTOs;

public record ShortenedUserDto(
	UserId Id,
	Username Name)
{
}
