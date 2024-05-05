using GamingManager.Domain.GameServerTickets.Events;
using GamingManager.Domain.Users;
using MediatR;

namespace GamingManager.Application.Features.GameServerTickets.Events;

public class GameServerTicketCreatedEventHandler(
	IUserRepository userRepository) : INotificationHandler<TicketCreatedEvent>
{
	public async Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
	{
		var users = await userRepository.GetAllAdmins().ToListAsync();
		foreach (var user in users)
		{
			Console.WriteLine($"Dear {user.Username.Value} ({user.Email.Value}): A new ticket '{notification.Title.Value}' has been created." +
								$"Please check the ticket overview for more information. LINK");
		}
	}
}
