using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Projects.Commands.Create;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Projects.Commands.Create;

public class CreateProjectCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository,
	IGameRepository gameRepository,
	IUserRepository userRepository) : ICommandHandler<CreateProjectCommand, CreateProjectResult>
{
	public async Task<CanFail<CreateProjectResult>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
	{
		var game = await gameRepository.GetAsync(request.GameName);
		if (game is null) return Errors.Games.NameNotFound;

		var user = await userRepository.GetAsync(request.AuditorId);
		if (user is null) return Errors.Users.IdNotFound;

		var projectResult = Project.Create(request.ProjectName, game, request.StartsAtUtc, user);
		if (projectResult.HasFailed) return projectResult.Errors;

		projectRepository.Add(projectResult.Value);

		var teamMember = projectResult.Value.Members.First(teamMember => teamMember.UserId == user.Id);

		await unitOfWork.SaveAsync(cancellationToken);

		return new CreateProjectResult(
			Id: projectResult.Value.Id.Value.ToString(),
			Name: projectResult.Value.Name.Value,
			Game: new CreateProjectGameResult(
				Id: game.Id.Value.ToString(),
				Name: game.Name.Value),
			Team: [
				new CreateProjectTeamMemberResult(
					Id: teamMember.Id.Value.ToString(),
					Role: teamMember.Role.ToString(),
					SinceUtc: teamMember.Since.Value,
					User: new CreateTeamMemberUserResult(
						Id: user.Id.Value.ToString(),
						Username: user.Username.Value))
				]);
	}
}
