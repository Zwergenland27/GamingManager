using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.RescheduleStart;

public class RescheduleProjectStartCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository) : ICommandHandler<RescheduleProjectStartCommand>
{
	public async Task<CanFail> Handle(RescheduleProjectStartCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		var result = project.RescheduleStart(request.PlannedStartUtc);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
