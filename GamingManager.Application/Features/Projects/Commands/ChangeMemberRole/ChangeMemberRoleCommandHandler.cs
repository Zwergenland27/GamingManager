using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.ChangeMemberRole;

public class ChangeMemberRoleCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository) : ICommandHandler<ChangeMemberRoleCommand>
{
	public async Task<CanFail> Handle(ChangeMemberRoleCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		var result = project.ChangeMemberRole(request.AuditorId, request.MemberId, request.NewRole);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
