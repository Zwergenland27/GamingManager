using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.Register;

public class RegisterUserCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository,
	IUserDtoRepository userDtoRepository) : ICommandHandler<RegisterUserCommand, DetailedUserDto>
{
	public async Task<CanFail<DetailedUserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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

		return (await userDtoRepository.GetDetailedAsync(request.Username))!;
	}
}
