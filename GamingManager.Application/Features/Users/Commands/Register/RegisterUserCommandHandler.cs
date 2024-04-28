using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.Commands.Register;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.Register;

public class RegisterUserCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository) : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
	public async Task<CanFail<RegisterUserResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		var usernameUnique = await userRepository.IsUsernameUnique(request.Username);
		if (!usernameUnique) return Errors.Users.DuplicateUsername;

		var emailUnique = await userRepository.IsEmailUnique(request.Email);
		if (!emailUnique) return Errors.Users.DuplicateEmail;

		var userResult = User.Create(request.Firstname, request.Lastname, request.Email, request.Username, request.Password);
		if (userResult.HasFailed) return userResult.Errors;

		userResult.Value.RequestEmailVerification();

		userRepository.Add(userResult.Value);

		await unitOfWork.SaveAsync(cancellationToken);

		return new RegisterUserResult(
			Id: userResult.Value.Id.Value.ToString(),
			Username: userResult.Value.Username.Value,
			Email: userResult.Value.Email.Value,
			Role: userResult.Value.Role.ToString(),
			Firstname: userResult.Value.Firstname?.Value,
			Lastname: userResult.Value.Lastname?.Value,
			EmailVerified: userResult.Value.EmailConfirmed);
	}
}
