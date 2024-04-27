using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.ChangePasword;

public class ChangePasswordCommandBuilder : IRequestBuilder<ChangePasswordParameters, ChangePasswordCommand>
{
	public ValidatedRequiredProperty<ChangePasswordCommand> Configure(RequiredPropertyBuilder<ChangePasswordParameters, ChangePasswordCommand> builder)
	{
		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.User.ChangePassword.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		var password = builder.ClassProperty(r => r.Password)
			.Required(Errors.User.ChangePassword.PasswordMissing)
			.Map(p => p.Password, Password.Create);

		return builder.Build(() => new ChangePasswordCommand(username, password));
	}
}

public record ChangePasswordCommand(
	Username Username,
	Password Password) : ICommand;
