using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Users.DTOs;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.Create;

public class CreateUserConfiguration : IRequestBuilder<CreateUserParameters, CreateUserCommand>
{
	public ValidatedRequiredProperty<CreateUserCommand> Configure(RequiredPropertyBuilder<CreateUserParameters, CreateUserCommand> builder)
	{
		var firstname = builder.ClassProperty(c => c.Firstname)
			.Optional()
			.Map(r => r.Firstname, value => new Firstname(value));

		var lastname = builder.ClassProperty(c => c.Lastname)
			.Optional()
			.Map(r => r.Lastname, value => new Lastname(value));

		var username = builder.ClassProperty(c => c.Username)
			.Required(Errors.Users.Create.UsernameMissing)
			.Map(r => r.Username, value => new Username(value));

		var email = builder.ClassProperty(c => c.Email)
			.Required(Errors.Users.Create.EmailMissing)
			.Map(r => r.Email, value => Email.Create(value));

		return builder.Build(() => new CreateUserCommand(firstname, lastname, username, email));
	}
}

public record CreateUserCommand(
    Firstname? Firstname,
    Lastname? Lastname,
    Username Username,
    Email Email) : ICommand<DetailedUserDto>;
