using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.Users;
using GamingManager.Domain.Users.ValueObjects;
using System.Reflection.Metadata.Ecma335;

namespace GamingManager.Application.Features.Users.DTOs;

public record DetailedUserDto(
	UserId Id,
	Firstname? Firstname,
	Lastname? Lastname,
	Email Email,
	Username Username,
	Role Role)
{
	public static DetailedUserDto FromUser(User user)
	{
		return new DetailedUserDto(
			user.Id,
			user.Firstname,
			user.Lastname,
			user.Email,
			user.Username,
			user.Role);
	}

	public DetailedUser Convert()
	{
		return new DetailedUser(
			Id.Value.ToString(),
			Firstname?.Value,
			Lastname?.Value,
			Email.Value,
			Username.Value,
			Role.ToString());
	}
}
