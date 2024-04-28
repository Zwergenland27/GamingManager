using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Projects.Commands.AddToTeam;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Projects.Commands.AddToTeam;

public class AddToTeamCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository,
	IUserRepository userRepository) : ICommandHandler<AddToTeamCommand, AddToTeamResult>
{
	public async Task<CanFail<AddToTeamResult>> Handle(AddToTeamCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if(project is null) return Errors.Projects.IdNotFound;

		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		var addResult = project.AddToTeam(user, request.Role);
		if(addResult.HasFailed) return addResult.Errors;

		var teamMember = project.Team.First(teamMember => teamMember.UserId == user.Id);

		await unitOfWork.SaveAsync(cancellationToken);

		return new AddToTeamResult(
			Id: teamMember.Id.Value.ToString(),
			Role: teamMember.Role.ToString(),
			SinceUtc: teamMember.Since.Value,
			User: new AddToTeamUserResult(
				Id: user.Id.Value.ToString(),
				Username: user.Username.Value));
	}
}
