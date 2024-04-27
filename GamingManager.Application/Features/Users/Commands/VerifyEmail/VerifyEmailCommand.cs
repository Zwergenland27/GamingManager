using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.VerifyEmail;

public class VerifyEmailCommandBuilder : IRequestBuilder<VerifyEmailParameters, VerifyEmailCommand>
{
	public ValidatedRequiredProperty<VerifyEmailCommand> Configure(RequiredPropertyBuilder<VerifyEmailParameters, VerifyEmailCommand> builder)
	{
		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.User.VerifyEmail.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		var validationToken = builder.ClassProperty(r => r.Token)
			.Required(Errors.User.VerifyEmail.TokenMissing)
			.Map(p => p.Token);

		return builder.Build(() => new VerifyEmailCommand(username, validationToken));
	}
}

public record VerifyEmailCommand(Username Username, string Token) : ICommand;
