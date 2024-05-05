using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServers.Commands.UseServer;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Servers;

namespace GamingManager.Application.Features.GameServers.Commands.UseServer;

public class UseServerCommandHandler(
	IUnitOfWork unitOfWork,
	IGameServerRepository gameServerRepository,
	IServerRepository serverRepository) : ICommandHandler<UseServerCommand, UseServerResult>
{
	public async Task<CanFail<UseServerResult>> Handle(UseServerCommand request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(request.Name);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		var server = await serverRepository.GetAsync(request.Hostname);
		if (server is null) return Errors.Servers.HostnameNotFound;

		var result = gameServer.Use(server);
		if(result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return new UseServerResult(
			Id: gameServer.Id.Value.ToString(),
			Hostname: server.Hostname.Value,
			Status: server.Status.ToString());
	}
}
