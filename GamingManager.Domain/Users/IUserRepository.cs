using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Users;

public interface IUserRepository
{
	void Add(User user);

	void Delete(User user);

	Task<User?> GetAsync(UserId id);
}
