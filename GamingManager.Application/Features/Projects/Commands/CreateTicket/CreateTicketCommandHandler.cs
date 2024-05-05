using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Projects.Commands.CreateTicket;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServerTickets;
using GamingManager.Domain.Projects;

namespace GamingManager.Application.Features.Projects.Commands.CreateTicket;

public class CreateTicketCommandHandler(
	IUnitOfWork unitOfWork,
	IProjectRepository projectRepository,
	IGameServerTicketRepository gameServerTicketRepository) : ICommandHandler<CreateTicketCommand, CreateTicketResult>
{
	public async Task<CanFail<CreateTicketResult>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(request.ProjectId);
		if (project is null) return Errors.Projects.IdNotFound;

		var ticketResult = project.CreateTicket(request.AuditorId, request.Title, request.Details);
		if (ticketResult.HasFailed) return ticketResult.Errors;

		gameServerTicketRepository.Add(ticketResult.Value);
		await unitOfWork.SaveAsync(cancellationToken);

		return new CreateTicketResult(
			ticketResult.Value.Id.Value.ToString(),
			ticketResult.Value.Title.Value,
			ticketResult.Value.Details.Value);
	}
}
