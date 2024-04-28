using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands.Login;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.Login;

public class LoginCommandBuilder : IRequestBuilder<LoginParameters, LoginCommand>
{
	public ValidatedRequiredProperty<LoginCommand> Configure(RequiredPropertyBuilder<LoginParameters, LoginCommand> builder)
	{
		var username = builder.ClassProperty(r => r.Username)
			.Optional()
			.Map(p => p.Username, value => new Username(value));

		var email = builder.ClassProperty(r => r.Email)
			.Optional()
			.Map(p => p.Email, Email.Create);

		var password = builder.ClassProperty(r => r.Password)
			.Required(Errors.User.Login.PasswordMissing)
			.Map(p => p.Password, Password.Create);

		return builder.Build(() => LoginCommand.Create(username, email, password));
	}
}

public record LoginCommand : ICommand<Tuple<string, RefreshToken>>
{
	private LoginCommand(Username? username, Email? email, Password password)
	{
		Username = username;
		Email = email;
		Password = password;
	}
	public Username? Username { get; private init; }

	public Email? Email { get; private init; }

	public Password Password { get; private init; }

	public string Secret { get; private set; }

	public static CanFail<LoginCommand> Create(Username? username, Email? email, Password password)
	{
		if(username is null && email is null) return Errors.User.Login.IdentifierMissing;
		return new LoginCommand(username, email, password);
	}
}
