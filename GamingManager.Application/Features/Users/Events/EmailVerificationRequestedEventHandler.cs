using GamingManager.Domain.Users.Events;
using MediatR;

namespace GamingManager.Application.Features.Users.Events;

public class EmailVerificationRequestedEventHandler : INotificationHandler<EmailVerificationRequestedEvent>
{
	public Task Handle(EmailVerificationRequestedEvent notification, CancellationToken cancellationToken)
	{
		var link = new Uri($"https://localhost:5001/api/Users/{notification.Username.Value}/Email/verify/{notification.ValidationToken}");
		Console.WriteLine($"Send email to user: {notification.Email.Value} Greetings, {notification.Username.Value}! Your verification link is the following: {link.ToString()}");

		return Task.CompletedTask;
	}
}
