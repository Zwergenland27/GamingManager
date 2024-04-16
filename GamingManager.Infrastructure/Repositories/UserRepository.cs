using GamingManager.Domain.Users;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class UserRepository(GamingManagerContext context) : IUserRepository
{
	private readonly GamingManagerContext _context = context;
	public void Add(User user)
	{
		_context.Users.Add(user);
	}

	public void Delete(User user)
	{
		_context.Users.Remove(user);
	}

	public async Task<User?> GetAsync(UserId id)
	{
		return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
	}

	public async Task<bool> IsEmailUnique(Email email)
	{
		return !await _context.Users.AnyAsync(user => user.Email == email);
	}

	public async Task<bool> IsUsernameUnique(Username username)
	{
		return !await _context.Users.AnyAsync(user => user.Username == username);
	}
}
