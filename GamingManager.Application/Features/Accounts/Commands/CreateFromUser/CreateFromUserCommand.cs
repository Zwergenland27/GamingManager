using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Accounts.Commands.CreateFromUser;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Commands.CreateFromLogin;

public class CreateFromUserCommandBuilder : IRequestBuilder<CreateFromUserParameters, CreateFromUserCommand>
{
	public ValidatedRequiredProperty<CreateFromUserCommand> Configure(RequiredPropertyBuilder<CreateFromUserParameters, CreateFromUserCommand> builder)
	{
		var gameName = builder.ClassProperty(r => r.GameName)
			.Required(Errors.Account.CreateFromUser.GameNameMissing)
			.Map(p => p.GameName, value => new GameName(value));

		var accountName = builder.ClassProperty(r => r.AccountName)
			.Required(Errors.Account.CreateFromUser.AccountNameMissing)
			.Map(p => p.AccountName, value => new AccountName(value));

		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.Account.CreateFromUser.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		return builder.Build(() => new CreateFromUserCommand(gameName, accountName, username));
	}
}

public record CreateFromUserCommand(
	GameName GameName,
	AccountName AccountName,
	Username Username) : ICommand<CreateFromUserResult>;
