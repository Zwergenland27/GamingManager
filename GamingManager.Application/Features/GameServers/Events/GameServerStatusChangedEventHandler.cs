using GamingManager.Domain.GameServers.Events;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Servers.ValueObjects;
using MediatR;

namespace GamingManager.Application.Features.GameServers.Events;

public class GameServerStatusChangedEventHandler(
	IServerRepository serverRepository) : INotificationHandler<GameServerStatusChangedEvent>
{
	public async Task Handle(GameServerStatusChangedEvent notification, CancellationToken cancellationToken)
	{
		if (notification.Status == GameServerStatus.Starting || notification.Status == GameServerStatus.Online)
		{
			await CancelServerShutdown(notification.Server);
		}

		if (notification.Status == GameServerStatus.Starting)
		{
			throw new NotImplementedException("TODO: send Start command via some API to the local server");
		}

		if (notification.Status == GameServerStatus.Offline)
		{
			await ScheduleServerShutdown(notification.Server);
		}
	}

	private async Task ScheduleServerShutdown(ServerId serverId)
	{
		var server = await serverRepository.GetAsync(serverId);
		if (server is null) throw new InvalidDataException("The server on which the game server is running does not exist");

		var hasActiveGameServers = await serverRepository.HasAnyActiveGameServersAsync(serverId);
		if(hasActiveGameServers) return;

		var shutdownAtUtc = new ServerShutdownAtUtc(DateTime.UtcNow.AddMinutes(server.ShutdownDelay.Minutes));
		server.ScheduleShutdown(shutdownAtUtc);
	}

	private async Task CancelServerShutdown(ServerId serverId)
	{
		var server = await serverRepository.GetAsync(serverId);
		if (server is null) throw new InvalidDataException("The server on which the game server is running does not exist");

		server.CancelShutdown();
	}
}
