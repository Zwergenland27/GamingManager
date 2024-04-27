using GamingManager.Domain.Users;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class UserRepository(GamingManagerContext context) : IUserRepository
{
	public void Add(User user)
	{
		context.Users.Add(user);
	}

	public void Delete(User user)
	{
		context.Users.Remove(user);
	}

	public async Task<User?> GetAsync(UserId id)
	{
		return await context.Users.FirstOrDefaultAsync(user => user.Id == id);
	}

	public async Task<User?> GetAsync(Username username)
	{
		return await context.Users.FirstOrDefaultAsync(user => user.Username == username);
	}

	public async Task<User?> GetAsync(Email email)
	{
		return await context.Users.FirstOrDefaultAsync(user => user.Email == email);
	}

	public async Task<bool> IsEmailUnique(Email email)
	{
		return !await context.Users.AnyAsync(user => user.Email == email);
	}

	public async Task<bool> IsUsernameUnique(Username username)
	{
		return !await context.Users.AnyAsync(user => user.Username == username);
	}
}
