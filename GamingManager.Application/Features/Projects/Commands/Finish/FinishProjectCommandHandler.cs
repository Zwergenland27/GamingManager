using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.Finish;

public class FinishProjectCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository) : ICommandHandler<FinishProjectCommand>
{
	public async Task<CanFail> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		project.Finish();

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
