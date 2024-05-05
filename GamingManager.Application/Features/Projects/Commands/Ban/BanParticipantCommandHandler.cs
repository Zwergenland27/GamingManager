using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Projects;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.Ban;

public class BanParticipantCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository) : ICommandHandler<BanParticipantCommand, BanResult>
{
	public async Task<CanFail<BanResult>> Handle(BanParticipantCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		CanFail<Domain.Projects.Entities.Ban> result = new();

		if (request.Duration is null)
		{
			result = project.BanPermanent(request.AuditorId, request.ParticipantId, request.Reason);
			if (result.HasFailed) return result.Errors;
		}
		else
		{
			result = project.BanTemporary(request.AuditorId, request.ParticipantId, request.Reason, request.Duration.Value);
			if (result.HasFailed) return result.Errors;
		}

		await unitOfWork.SaveAsync(cancellationToken);

		var ban = result.Value;

		return new BanResult(
			Id: ban.Id.Value.ToString(),
			Reason: ban.Reason.Value,
			BannedAtUtc: ban.BannedAtUtc,
			Duration: ban.Duration);
	}
}
