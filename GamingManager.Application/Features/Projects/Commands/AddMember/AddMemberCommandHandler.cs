using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Projects.Commands.AddToTeam;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Projects.Commands.AddMember;

public class AddMemberCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository,
	IUserRepository userRepository) : ICommandHandler<AddMemberCommand, AddMemberResult>
{
	public async Task<CanFail<AddMemberResult>> Handle(AddMemberCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		var addResult = project.AddMember(request.AuditorId, user, request.Role);
		if (addResult.HasFailed) return addResult.Errors;

		var teamMember = project.Members.First(teamMember => teamMember.UserId == user.Id);

		await unitOfWork.SaveAsync(cancellationToken);

		return new AddMemberResult(
			Id: teamMember.Id.Value.ToString(),
			Role: teamMember.Role.ToString(),
			SinceUtc: teamMember.Since.Value,
			User: new AddToTeamUserResult(
				Id: user.Id.Value.ToString(),
				Username: user.Username.Value));
	}
}
