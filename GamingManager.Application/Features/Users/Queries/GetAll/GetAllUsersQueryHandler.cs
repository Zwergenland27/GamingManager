using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.DTOs;

namespace GamingManager.Application.Features.Users.Queries.GetAll;

public class GetAllUsersQueryHandler(
	IUserDtoRepository userDtoRepository) : IQueryHandler<GetAllUsersQuery, IEnumerable<ShortenedUserDto>>
{
	public async Task<CanFail<IEnumerable<ShortenedUserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
	{
		return await userDtoRepository.GetAllAsync().ToListAsync(cancellationToken);
	}
}
