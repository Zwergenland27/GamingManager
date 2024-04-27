using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands;
using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.Register;

public class RegisterUserCommandBuilder : IRequestBuilder<RegisterUserParameters, RegisterUserCommand>
{
	public ValidatedRequiredProperty<RegisterUserCommand> Configure(RequiredPropertyBuilder<RegisterUserParameters, RegisterUserCommand> builder)
	{
		var firstname = builder.ClassProperty(c => c.Firstname)
			.Optional()
			.Map(r => r.Firstname, value => new Firstname(value));

		var lastname = builder.ClassProperty(c => c.Lastname)
			.Optional()
			.Map(r => r.Lastname, value => new Lastname(value));

		var username = builder.ClassProperty(c => c.Username)
			.Required(Errors.User.Register.UsernameMissing)
			.Map(r => r.Username, value => new Username(value));

		var email = builder.ClassProperty(c => c.Email)
			.Required(Errors.User.Register.EmailMissing)
			.Map(r => r.Email, Email.Create);

		var password = builder.ClassProperty(c => c.Password)
			.Required(Errors.User.Register.PasswordMissing)
			.Map(r => r.Password, Password.Create);

		return builder.Build(() => new RegisterUserCommand(firstname, lastname, username, email, password));
	}
}

public record RegisterUserCommand(
	Firstname? Firstname,
	Lastname? Lastname,
	Username Username,
	Email Email,
	Password Password) : ICommand<DetailedUserDto>;
