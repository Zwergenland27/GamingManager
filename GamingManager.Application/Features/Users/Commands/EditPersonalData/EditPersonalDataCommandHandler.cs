using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Users.Commands.EditPersonalData;

public class EditPersonalDataCommandHandler(
	IUnitOfWork unitOfWork,
	IUserRepository userRepository) : ICommandHandler<EditPersonalDataCommand>
{
	public async Task<CanFail> Handle(EditPersonalDataCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetAsync(request.CurrentUsername);
		if (user is null) return Errors.Users.UsernameNotFound;

		var result = user.EditPersonalData(request.NewUsername, request.Firstname, request.Lastname, request.Email);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
