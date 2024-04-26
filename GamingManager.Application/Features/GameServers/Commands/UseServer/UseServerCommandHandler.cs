using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Servers;

namespace GamingManager.Application.Features.GameServers.Commands.UseServer;

public class UseServerCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerRepository gameServerRepository,
	IServerRepository serverRepository) : ICommandHandler<UseServerCommand>
{
	public async Task<CanFail> Handle(UseServerCommand request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(request.Name);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		var server = await serverRepository.GetAsync(request.Hostname);
		if (server is null) return Errors.Servers.HostnameNotFound;

		gameServer.Use(server);

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
