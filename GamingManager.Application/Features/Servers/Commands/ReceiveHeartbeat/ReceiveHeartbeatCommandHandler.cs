using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.ReceiveHeartbeat;

public class ReceiveHeartbeatCommandHandler(
	IUnitOfWork unitOfWork,
	IServerRepository serverRepository) : ICommandHandler<ReceiveHeartbeatCommand>
{
	public async Task<CanFail> Handle(ReceiveHeartbeatCommand request, CancellationToken cancellationToken)
	{
		var server = await serverRepository.GetAsync(request.Hostname);
		if(server is null) return Errors.Servers.HostnameNotFound;

		var heartbeatResult = server.Heartbeat(new HeartbeatReceivedAtUtc(DateTime.UtcNow));
		if(heartbeatResult.HasFailed) return heartbeatResult.Errors;

		await unitOfWork.SaveAsync(cancellationToken);
		return CanFail.Success();
	}
}
