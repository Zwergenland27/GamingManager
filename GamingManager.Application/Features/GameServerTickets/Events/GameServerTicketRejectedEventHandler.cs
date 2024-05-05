using GamingManager.Domain.GameServerTickets.Events;
using GamingManager.Domain.Users;
using MediatR;

namespace GamingManager.Application.Features.GameServerTickets.Events;

public class GameServerTicketRejectedEventHandler(
	IUserRepository userRepository) : INotificationHandler<TicketRejectedEvent>
{
	public async Task Handle(TicketRejectedEvent notification, CancellationToken cancellationToken)
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

		Console.WriteLine($"Dear {applicant.Username.Value} ({applicant.Email.Value}): Your ticket '{notification.Title.Value}' has been rejected for the following reason: {notification.Reason.Value}." +
			$"If you have any questions, feel free to contact {issuer.Username.Value} ({issuer.Email.Value})");
	}
}
