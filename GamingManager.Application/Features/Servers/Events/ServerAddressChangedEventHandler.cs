using GamingManager.Domain.GameServers;
using GamingManager.Domain.Servers.Events;
using MediatR;

namespace GamingManager.Application.Features.Servers.Events;

public class ServerAddressChangedEventHandler(
	IGameServerRepository  gameServerRepository) : INotificationHandler<ServerAddressChangedEvent>
{
	public async Task Handle(ServerAddressChangedEvent notification, CancellationToken cancellationToken)
	{
		var gameServers = await gameServerRepository.GetAllOfServerAsync(notification.Server).ToListAsync();

		gameServers.ForEach(gameServer =>
		{
			gameServer.HostAddressChanged(notification.NewAddress);
		});
	}
}
