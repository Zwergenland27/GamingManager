using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.Queries.Get;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Users.Queries.Get;

public class GetUserQueryHandler(
	IUserDtoRepository userDtoRepository) : IQueryHandler<GetUserQuery, GetUserResult>
{
	public async Task<CanFail<GetUserResult>> Handle(GetUserQuery request, CancellationToken cancellationToken)
	{
		var user = await userDtoRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		return user;
	}
}
