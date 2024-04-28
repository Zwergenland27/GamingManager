using GamingManager.Contracts.Features.Users.Queries.Get;
using GamingManager.Contracts.Features.Users.Queries.GetAll;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users;

public interface IUserDtoRepository
{
	Task<GetUserResult?> GetAsync(Username username);

	IAsyncEnumerable<GetAllUsersResult> GetAllAsync();
}
