using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.RefreshJwtToken;

public class RefreshJwtTokenCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository,
	ITokenGenerator tokenGenerator) : ICommandHandler<RefreshJwtTokenCommand, Tuple<string, RefreshToken>>
{
	public async Task<CanFail<Tuple<string, RefreshToken>>> Handle(RefreshJwtTokenCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		var refreshTokenValid = user.IsRefreshTokenValid(request.RefreshToken);
		if(!refreshTokenValid) return Errors.Users.InvalidRefreshToken;

		var jwtToken = tokenGenerator.GenerateJwtToken(user.Id, user.Username, user.Role);
		var refreshToken = tokenGenerator.GenerateRefreshToken();

		user.SetRefreshToken(refreshToken);

		await unitOfWork.SaveAsync(cancellationToken);

		return new Tuple<string, RefreshToken>(jwtToken, refreshToken);
	}
}
