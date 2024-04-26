using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users;

public static class Converters
{
	public static ShortenedUserDto ToDto(this User user)
	{
		return new ShortenedUserDto(
			user.Id.Value.ToString(),
			user.Username.Value);
	}
}
