using GamingManager.Application.Features.GameServers;
using GamingManager.Contracts.Features.GameServers.Queries.Get;
using GamingManager.Domain.GameServers.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class GameServerDtoRepository(GamingManagerReadContext context) : IGameServerDtoRepository
{
	public async Task<GetGameServerResult?> GetAsync(GameServerName gameServerName)
	{
		return await context.GameServers
			.Include(gameServer => gameServer.Project)
			.Include(gameServer => gameServer.HostedOn)
			.Where(gameServer => gameServer.ServerName == gameServerName.Value)
			.Select(gameServer => new GetGameServerResult(
				gameServer.Id.ToString(),
				gameServer.ServerName,
				gameServer.ShutdownDelay,
				gameServer.Address,
				gameServer.Status.ToString(),
				new GetGameServerProjectResult(
					gameServer.Project.Id.ToString(),
					gameServer.Project.Name),
				ReferenceEquals(gameServer.HostedOn, null) ? null : new GetGameServerServerResult(
					gameServer.HostedOn.Id.ToString(),
					gameServer.HostedOn.Hostname,
					gameServer.HostedOn.Status.ToString())))
			.FirstOrDefaultAsync();
	}
}
