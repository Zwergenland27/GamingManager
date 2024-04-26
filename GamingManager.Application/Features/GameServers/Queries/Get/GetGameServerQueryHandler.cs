using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServers.DTOs;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.GameServers.Queries.Get;

public class GetGameServerQueryHandler(
	IGameServerDtoRepository gameServerDtoRepository) : IQueryHandler<GetGameServerQuery, DetailedGameServerDto>
{
	public async Task<CanFail<DetailedGameServerDto>> Handle(GetGameServerQuery request, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerDtoRepository.GetDetailedAsync(request.GameServerName);
		if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

		return gameServer;
	}
}
