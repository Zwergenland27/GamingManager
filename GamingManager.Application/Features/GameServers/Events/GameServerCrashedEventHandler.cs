using GamingManager.Domain.GameServers.Events;
using GamingManager.Domain.Projects;
using MediatR;

namespace GamingManager.Application.Features.GameServers.Events;

public class GameServerCrashedEventHandler(
	IProjectRepository projectRepository) : INotificationHandler<GameServerCrashedEvent>
{
	public async Task Handle(GameServerCrashedEvent notification, CancellationToken cancellationToken)
	{
		var project = await projectRepository.GetAsync(notification.Project);

		if (project is null)
		{
			throw new InvalidDataException("The project for the crashed server does not exist");
		}

		var crashedResult = project.Crashed(notification.CrashedAtUtc);
		if (crashedResult.HasFailed)
		{
			throw new InvalidDataException("Some error occured while calling crashed on the project");
		}
	}
}
