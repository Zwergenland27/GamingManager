using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.RequestEmailVerification;

public class RequestEmailVerificationCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository) : ICommandHandler<RequestEmailVerificationCommand>
{
	public async Task<CanFail> Handle(RequestEmailVerificationCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		user.RequestEmailVerification();

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
