using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository) : ICommandHandler<DeleteUserCommand>
{
	public async Task<CanFail> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		user.Delete();

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
