using GamingManager.Domain.Users.Events;
using MediatR;

namespace GamingManager.Application.Features.Users.Events;

public class PasswordResettedEventHandler : INotificationHandler<PasswordResettedEvent>
{
	public Task Handle(PasswordResettedEvent notification, CancellationToken cancellationToken)
	{
		Console.WriteLine($"Send email to user: {notification.Email.Value} Greetings, {notification.Username.Value}! Your new password is {notification.NewPassword.Value}");

		return Task.CompletedTask;
	}
}
