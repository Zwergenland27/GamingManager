using GamingManager.Application.Features.Games;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Application.Features.Servers;
using GamingManager.Contracts.Features.GameServers.DTOs;
using GamingManager.Contracts.Features.Servers.DTOs;
using GamingManager.Domain.Servers.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class ServerDtoRepository(GamingManagerContext context) : IServerDtoRepository
{
	public IAsyncEnumerable<ShortenedServerDto> GetAllAsync()
	{
		return context.Servers
			.AsNoTracking()
			.Select(server => server.ToDto())
			.AsAsyncEnumerable();
	}

	public async  Task<DetailedServerDto?> GetDetailedAsync(Hostname hostname)
	{
		var server = await context.Servers
			.AsNoTracking()
			.GroupJoin(context.GameServers.AsNoTracking(),
				server => server.Id,
				gameServer => gameServer.HostedOn,
				(server, gameServers) => new
				{
					server.Id,
					server.Hostname,
					server.Mac,
					server.Address,
					GameServers = gameServers
				})
			.FirstOrDefaultAsync(server => server.Hostname == hostname);

		if (server == null) return null;

		var gameServers = server.GameServers
			.Join(context.Projects.AsNoTracking(),
				gameServer => gameServer.Project,
				project => project.Id,
				(gameServer, project) => new
				{
					gameServer.Id,
					gameServer.ServerName,
					Project = project
				})
			.Join(context.Games.AsNoTracking(),
				prevJoin => prevJoin.Project.Game,
				game => game.Id,
				(prevJoin, game) => new ShortenedGameServerDto(
					prevJoin.Id.Value.ToString(),
					prevJoin.ServerName.Value,
					new ShortenedProjectDto(
						prevJoin.Project.Id.Value.ToString(),
						prevJoin.Project.Name.Value,
						game.ToDto())))
			.ToList();

		return new DetailedServerDto(
			server.Id.Value.ToString(),
			server.Hostname.Value,
			server.Mac.Value,
			server.Address.ToString(),
			gameServers);
	}
}
