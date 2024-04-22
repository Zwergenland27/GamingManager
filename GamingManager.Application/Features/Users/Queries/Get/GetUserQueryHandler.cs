using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Users.Queries.Get;

public class GetUserQueryHandler(
	IUserDtoRepository userDtoRepository) : IQueryHandler<GetUserQuery, DetailedUserDto>
{
	public async Task<CanFail<DetailedUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
	{
		var user = await userDtoRepository.GetDetailedAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		return user;
	}
}
