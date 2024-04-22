using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users;

public interface IUserDtoRepository
{
	Task<DetailedUserDto?> GetDetailedAsync(Username Username);

	IAsyncEnumerable<ShortenedUserDto> GetAllAsync();
}
