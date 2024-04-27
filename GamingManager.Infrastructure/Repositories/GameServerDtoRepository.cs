using GamingManager.Application.Features.Games;
using GamingManager.Application.Features.GameServers;
using GamingManager.Application.Features.Projects.Commands.SetPlannedEnd;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Contracts.Features.GameServers.DTOs;
using GamingManager.Contracts.Features.Servers.DTOs;
using GamingManager.Domain.GameServers.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class GameServerDtoRepository(GamingManagerContext context) : IGameServerDtoRepository
{
	public IAsyncEnumerable<ShortenedGameServerDto> GetAllAsync()
	{
		return context.GameServers
			.AsNoTracking()
			.Join(context.Projects.AsNoTracking(),
				gameServer => gameServer.Project,
				project => project.Id,
				(gameServer, project) => new { GameServer = gameServer, Project = project })
			.Join(context.Games.AsNoTracking(),
				prevJoin => prevJoin.Project.Game,
				game => game.Id,
				(prevJoin, game) => new ShortenedGameServerDto(
					prevJoin.GameServer.Id.Value.ToString(),
					prevJoin.GameServer.ServerName.Value,
					new ShortenedProjectDto(
						prevJoin.Project.Id.Value.ToString(),
						prevJoin.Project.Name.Value,
						game.ToDto()
						)))
			.AsAsyncEnumerable();
	}

	public async Task<DetailedGameServerDto?> GetDetailedAsync(GameServerName gameServerName)
	{
		///TODO: Refactor for better performance
		var gameServer = await context.GameServers
			.AsNoTracking()
			.FirstOrDefaultAsync(gameServer => gameServer.ServerName == gameServerName);

		if (gameServer is null) return null;

		var project = await context.Projects
			.AsNoTracking()
			.Where(project => project.Id == gameServer.Project)
			.Join(context.Games.AsNoTracking(),
				project => project.Game,
				game => game.Id,
				(project, game) => new ShortenedProjectDto(
					project.Id.Value.ToString(),
					project.Name.Value,
					game.ToDto()
					))
			.FirstAsync();

		var server = await context.Servers
			.AsNoTracking()
			.Where(server => server.Id == gameServer.HostedOn)
			.Select(server => new ShortenedServerDto(
				server.Id.Value.ToString(),
				server.Hostname.Value))
			.FirstOrDefaultAsync();

		return new DetailedGameServerDto(
			gameServer.Id.Value.ToString(),
			gameServer.ServerName.Value,
			gameServer.ShutdownDelay.Minutes,
			project,
			server);
	}
}
