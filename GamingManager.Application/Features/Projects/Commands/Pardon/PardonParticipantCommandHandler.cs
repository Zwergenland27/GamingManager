using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.Pardon;

public class PardonParticipantCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository) : ICommandHandler<PardonParticipantCommand>
{
	public async Task<CanFail> Handle(PardonParticipantCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		var result = project.Pardon(request.AuditorId, request.ParticipantId);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
