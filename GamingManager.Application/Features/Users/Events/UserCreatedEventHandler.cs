using GamingManager.Domain.Users.Events;
using MediatR;

namespace GamingManager.Application.Features.Users.Events;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
	public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
	{
		Console.WriteLine($"Send email to user: {notification.Email.Value} Greetings, {notification.Username.Value}");

		return Task.CompletedTask;
	}
}
