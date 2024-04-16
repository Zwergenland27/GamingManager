using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Projects.Commands.AddToTeam;

public class AddToTeamCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository,
	IUserRepository userRepository) : ICommandHandler<AddToTeamCommand>
{
	public async Task<CanFail> Handle(AddToTeamCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if(project is null) return Errors.Projects.IdNotFound;

		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		var addResult = project.AddToTeam(user, request.Role);
		if(addResult.HasFailed) return addResult.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
