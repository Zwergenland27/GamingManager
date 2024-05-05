using GamingManager.Domain.GameServers;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.Events;
using MediatR;

namespace GamingManager.Application.Features.Projects.Events;

public class ParticipantLeftEventHandler(
	IGameServerRepository gameServerRepository) : INotificationHandler<ParticipantLeftEvent>
{
	public async Task Handle(ParticipantLeftEvent notification, CancellationToken cancellationToken)
	{
		var gameServer = await gameServerRepository.GetAsync(notification.GameServer);
		if (gameServer is null) throw new InvalidDataException("The player left a project which game server does not exist.");

		if(notification.IsLast)
		{
			var shutdownAtUtc = new GameServerShutdownAtUtc(DateTime.UtcNow.AddMinutes(gameServer.ShutdownDelay.Minutes));
			gameServer.ScheduleShutdown(shutdownAtUtc);
		}
	}
}
