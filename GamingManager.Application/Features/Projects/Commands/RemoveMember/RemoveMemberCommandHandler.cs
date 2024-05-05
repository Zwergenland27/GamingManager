using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.RemoveMember;

public class RemoveMemberCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository) : ICommandHandler<RemoveMemberCommand>
{
	public async Task<CanFail> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		var addResult = project.RemoveMember(request.AuditorId, request.MemberId);
		if (addResult.HasFailed) return addResult.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
