using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServerTickets;

namespace GamingManager.Application.Features.GameServerTickets.Commands.Reject;

public class RejectGameServerTicketCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerTicketRepository gameServerTicketRepository) : ICommandHandler<RejectGameServerTicketCommand>
{
	public async Task<CanFail> Handle(RejectGameServerTicketCommand request, CancellationToken cancellationToken)
	{
		var ticket = await gameServerTicketRepository.GetAsync(request.Id);
		if (ticket is null) return Errors.GameServerTickets.IdNotFound;

		var result = ticket.Reject(request.AuditorId, request.Reason);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
