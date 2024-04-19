using GamingManager.Domain.GameServers;
using GamingManager.Domain.Projects.Events;
using MediatR;

namespace GamingManager.Application.Features.Projects.Events;

public class ParticipantJoinedEventHandler(
	IGameServerRepository gameServerRepository) : INotificationHandler<ParticipantJoinedEvent>
{
	public async Task Handle(ParticipantJoinedEvent notification, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(notification.GameServer);
		if (gameServer is null) throw new InvalidDataException("The player joined a project which game server does not exist.");

		gameServer.CancelShutdown();
	}
}
