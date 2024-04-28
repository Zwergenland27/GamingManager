using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands.EditPersonalData;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.EditPersonalData;

public class EditPersonalDataCommandBuilder : IRequestBuilder<EditPersonalDataParameters, EditPersonalDataCommand>
{
	public ValidatedRequiredProperty<EditPersonalDataCommand> Configure(RequiredPropertyBuilder<EditPersonalDataParameters, EditPersonalDataCommand> builder)
	{
		var currentUsername = builder.ClassProperty(r => r.CurrentUsername)
			.Required(Errors.User.EditPersonalData.CurrentUsernameMissing)
			.Map(p => p.CurrentUsername, value => new Username(value));

		var newUsername = builder.ClassProperty(r => r.NewUsername)
			.Optional()
			.Map(p => p.NewUsername, value => new Username(value));

		var firstname = builder.ClassProperty(r => r.Firstname)
			.Optional()
			.Map(p => p.Firstname, value => new Firstname(value));

		var lastname = builder.ClassProperty(r => r.Lastname)
			.Optional()
			.Map(p => p.Lastname, value => new Lastname(value));

		var email = builder.ClassProperty(r => r.Email)
			.Optional()
			.Map(p => p.Email, Email.Create);

		return builder.Build(() => new EditPersonalDataCommand(currentUsername, newUsername, firstname, lastname, email));
	}
}

public record EditPersonalDataCommand(
	Username CurrentUsername,
	Username? NewUsername,
	Firstname? Firstname,
	Lastname? Lastname,
	Email? Email) : ICommand;
