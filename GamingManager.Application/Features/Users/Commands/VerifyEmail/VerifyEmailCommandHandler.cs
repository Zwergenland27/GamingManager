using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.VerifyEmail;

public class VerifyEmailCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository) : ICommandHandler<VerifyEmailCommand>
{
	public async Task<CanFail> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		var result = user.ConfirmEmail(request.Token);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
