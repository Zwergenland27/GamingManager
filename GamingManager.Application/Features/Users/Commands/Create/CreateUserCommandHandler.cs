using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.Create;

public class CreateUserCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository) : ICommandHandler<CreateUserCommand, DetailedUserDto>
{
	public async Task<CanFail<DetailedUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var usernameUnique = await userRepository.IsUsernameUnique(request.Username);
		if (!usernameUnique) return Errors.Users.DuplicateUsername;

		var emailUnique = await userRepository.IsEmailUnique(request.Email);
		if (!emailUnique) return Errors.Users.DuplicateEmail;

		var userResult = User.Create(request.Firstname, request.Lastname, request.Email, request.Username);
		if(userResult.HasFailed) return userResult.Errors;

		userRepository.Add(userResult.Value);

		await unitOfWork.SaveAsync(cancellationToken);
		
		return DetailedUserDto.FromUser(userResult.Value);
	}
}
