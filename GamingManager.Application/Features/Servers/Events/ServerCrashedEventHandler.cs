using GamingManager.Domain.GameServers;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Servers.Events;
using MediatR;

namespace GamingManager.Application.Features.Servers.Events;

public class ServerCrashedEventHandler(
	IGameServerRepository gameServerRepository) : INotificationHandler<ServerCrashedEvent>
{
	public async Task Handle(ServerCrashedEvent notification, CancellationToken cancellationToken)
	{
		var runningGameServers = await gameServerRepository.GetAllOnlineAsync(notification.Server).ToListAsync();

		runningGameServers.ForEach(gameServer =>
		{
			gameServer.Crashed(new GameServerCrashedAtUtc(notification.CrashedAt.Value));
		});
	}
}
