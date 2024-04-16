using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Servers;

namespace GamingManager.Application.Features.Servers.Commands.Delete;

public class DeleteServerCommandHandler(
	IUnitOfWork unitOfWork,
	IServerRepository serverRepository) : ICommandHandler<DeleteServerCommand>
{
	public async Task<CanFail> Handle(DeleteServerCommand request, CancellationToken cancellationToken)
	{
		var server = await serverRepository.GetAsync(request.Hostname);
		if(server is null) return Errors.Servers.HostnameNotFound;

		var isUsed = await serverRepository.HostsGameServer(server.Id);
		if(isUsed) return Errors.Servers.InUse;

		serverRepository.Delete(server);

		await unitOfWork.SaveAsync(cancellationToken);
		return CanFail.Success();
	}
}
