using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.RequestPasswordReset;

public class RequestPasswordResetCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository) : ICommandHandler<RequestPasswordResetCommand>
{
	public async Task<CanFail> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetAsync(request.Email);
		if (user is null) return Errors.Users.InvalidLogin;

		user.RequestPasswordReset();

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
