using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.Queries.GetAll;

namespace GamingManager.Application.Features.Users.Queries.GetAll;

public class GetAllUsersQueryHandler(
	IUserDtoRepository userDtoRepository) : IQueryHandler<GetAllUsersQuery, IEnumerable<GetAllUsersResult>>
{
	public async Task<CanFail<IEnumerable<GetAllUsersResult>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
	{
		return await userDtoRepository.GetAllAsync().ToListAsync(cancellationToken);
	}
}
