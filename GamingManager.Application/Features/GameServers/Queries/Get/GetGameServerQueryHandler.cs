using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServers.Queries.Get;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.GameServers.Queries.Get;

public class GetGameServerQueryHandler(
	IGameServerDtoRepository gameServerDtoRepository) : IQueryHandler<GetGameServerQuery, GetGameServerResult>
{
	public async Task<CanFail<GetGameServerResult>> Handle(GetGameServerQuery request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerDtoRepository.GetAsync(request.GameServerName);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		return gameServer;
	}
}
