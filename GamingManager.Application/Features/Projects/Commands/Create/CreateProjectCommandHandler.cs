using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Projects.Commands.Create;

public class CreateProjectCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository,
	IProjectDtoRepository projectDtoRepository,
	IGameRepository gameRepository,
	IUserRepository userRepository) : ICommandHandler<CreateProjectCommand, DetailedProjectDto>
{
	public async Task<CanFail<DetailedProjectDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
	{
		var game = await gameRepository.GetAsync(request.GameName);
		if(game is null) return Errors.Games.NameNotFound;

		var user = await userRepository.GetAsync(request.Username);
		if(user is null) return Errors.Users.IdNotFound;


		var projectResult = Project.Create(request.ProjectName, game, request.StartsAtUtc, user);
		if (projectResult.HasFailed) return projectResult.Errors;

		projectRepository.Add(projectResult.Value);

		await unitOfWork.SaveAsync(cancellationToken);

		return (await projectDtoRepository.GetDetailedAsync(projectResult.Value.Id))!;
	}
}
