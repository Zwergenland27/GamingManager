using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Users;

public interface IUserRepository
{
	void Add(User user);

	void Delete(User user);

	Task<bool> IsUsernameUnique(Username username);

	Task<bool> IsEmailUnique(Email email);

	Task<User?> GetAsync(UserId id);

	Task<User?> GetAsync(Username username);
}
