using GamingManager.Application.Features.Games;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Application.Features.Users;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class UserDtoRepository(GamingManagerContext context) : IUserDtoRepository
{
	public IAsyncEnumerable<ShortenedUserDto> GetAllAsync()
	{
		return context.GetUsersAsync(user => true);
	}

	public async Task<DetailedUserDto?> GetDetailedAsync(Username username)
	{
		var user = await context.Users.AsNoTracking()
			.GroupJoin(context.Accounts.AsNoTracking(),
				user => user.Id,
				account => account.User,
				(user, accounts) => new
				{
					user.Id,
					user.Username,
					user.Email,
					user.Firstname,
					user.Lastname,
					user.Role,
					Accounts = accounts
				})
			.FirstOrDefaultAsync(user => user.Username == username);

		if (user is null) return null;

		var accounts = user.Accounts
			.Join(context.Games.AsNoTracking(),
				account => account.Game,
				game => game.Id,
				(account, game) => new ShortenedAccountDto(
					account.Id.Value.ToString(),
					account.Name.ToString()))
			.ToList();

		var memberOf = await context.Projects.AsNoTracking()
			.Where(project => project.Team.Any(user => user.Id == user.Id))
			.Join(context.Games.AsNoTracking(),
				project => project.Game,
				game => game.Id,
				(project, game) => new ShortenedProjectDto(
					project.Id.Value.ToString(),
					project.Name.Value,
					game.ToDto()))
			.ToListAsync();

		return new DetailedUserDto(
			user.Id.Value.ToString(),
			user.Firstname?.Value,
			user.Lastname?.Value,
			user.Email.Value,
			user.Username.Value,
			user.Role.ToString(),
			accounts,
			memberOf);
	}
}
