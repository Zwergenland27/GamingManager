using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.GameServerTickets;

namespace GamingManager.Application.Features.GameServerTickets.Commands.Accept;

public class AcceptGameServerTicketCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerTicketRepository gameServerTicketRepository,
	IGameServerRepository gameServerRepository) : ICommandHandler<AcceptGameServerTicketCommand>
{
	public async Task<CanFail> Handle(AcceptGameServerTicketCommand request, CancellationToken cancellationToken)
	{
		var ticket = await gameServerTicketRepository.GetAsync(request.Id);
		if (ticket is null) return Errors.GameServerTickets.IdNotFound;

		var gameServer = await gameServerRepository.GetAsync(request.GameServerName);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		var result = ticket.Accept(request.AuditorId, gameServer.Id);
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
