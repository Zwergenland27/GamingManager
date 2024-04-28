using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.Login;

internal class LoginCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository,
	ITokenGenerator tokenGenerator) : ICommandHandler<LoginCommand, Tuple<string, RefreshToken>>
{
	public async Task<CanFail<Tuple<string, RefreshToken>>> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		User? user = null;
		if (request.Username is not null)
		{
			user = await userRepository.GetAsync(request.Username);
		}

		if (request.Email is not null)
		{
			user = await userRepository.GetAsync(request.Email);
		}

		if (user is null) return Errors.Users.InvalidLogin;
		if (!user.IsPasswordCorret(request.Password)) return Errors.Users.InvalidLogin;

		var jwtToken = tokenGenerator.GenerateJwtToken(user.Id, user.Username, user.Role);
		var refreshToken = tokenGenerator.GenerateRefreshToken();

		user.SetRefreshToken(refreshToken);

		await unitOfWork.SaveAsync(cancellationToken);

		return new Tuple<string, RefreshToken>(jwtToken, refreshToken);
	}
}
