using GamingManager.Domain.GameServers;
using GamingManager.Domain.Servers.Events;
using GamingManager.Domain.Servers.ValueObjects;
using MediatR;

namespace GamingManager.Application.Features.Servers.Events;

public class ServerStatusChangedEventHandler(
	IGameServerRepository gameServerRepository) : INotificationHandler<ServerStatusChangedEvent>
{
	public async Task Handle(ServerStatusChangedEvent notification, CancellationToken cancellationToken)
	{
		if (notification.Status == ServerStatus.Online)
		{
			await StartAllGameServers(notification.Server);
		}

		if (notification.Status == ServerStatus.Starting)
		{
			throw new NotImplementedException("TODO: start hangfire job that sends WOL packets and set startFailed if server could not be started");
		}
	}

	private async Task StartAllGameServers(ServerId serverId)
	{
		var startableGameServers = await gameServerRepository.GetAllStartablesAsync(serverId).ToListAsync();

		startableGameServers.ForEach(gameServer =>
		{
			gameServer.Start();
		});
	}
}
