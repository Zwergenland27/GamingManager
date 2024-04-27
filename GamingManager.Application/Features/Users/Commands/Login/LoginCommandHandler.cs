using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.Login;

internal class LoginCommandHandler(
	IUserRepository userRepository,
	IJwtTokenGenerator jwtTokenGenerator) : ICommandHandler<LoginCommand, string>
{
	public async Task<CanFail<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
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


		return jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Role);
	}
}
