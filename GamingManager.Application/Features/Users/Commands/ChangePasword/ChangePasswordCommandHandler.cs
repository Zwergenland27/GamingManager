using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.ChangePasword;

public class ChangePasswordCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository) : ICommandHandler<ChangePasswordCommand>
{
	public async Task<CanFail> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		user.ChangePassword(request.Password);

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}