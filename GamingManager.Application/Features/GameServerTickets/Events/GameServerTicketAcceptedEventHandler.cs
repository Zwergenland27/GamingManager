using GamingManager.Domain.GameServers;
using GamingManager.Domain.GameServerTickets.Events;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Users;
using MediatR;

namespace GamingManager.Application.Features.GameServerTickets.Events;

public class GameServerTicketAcceptedEventHandler(
	IUserRepository userRepository,
	IProjectRepository projectRepository) : INotificationHandler<TicketAcceptedEvent>
{
	public async Task Handle(TicketAcceptedEvent notification, CancellationToken cancellationToken)
	{
		var applicant = await userRepository.GetAsync(notification.ApplicantId);
		if (applicant is null)
		{
			throw new InvalidDataException("The applicant does not exist");
		}

		var issuer = await userRepository.GetAsync(notification.IssuerId);
		if (issuer is null)
		{
			throw new InvalidDataException("The issuer does not exist");
		}

		var project = await projectRepository.GetAsync(notification.ProjectId);
		if (project is null)
		{
			throw new InvalidDataException("The project does not exist");
		}

		project.SetServer(notification.GameServerId);

		Console.WriteLine($"Dear {applicant.Username.Value} ({applicant.Email.Value}): Your ticket '{notification.Title.Value}' has been accepted." +
			$"If you have any questions, feel free to contact {issuer.Username.Value} ({issuer.Email.Value})");
	}
}
