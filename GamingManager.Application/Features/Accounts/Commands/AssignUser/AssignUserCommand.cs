using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Accounts.Commands.AssignUser;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Commands.AssignUser;

public class AssignUserCommandBuilder : IRequestBuilder<AssignUserParameters, AssignUserCommand>
{
	public ValidatedRequiredProperty<AssignUserCommand> Configure(RequiredPropertyBuilder<AssignUserParameters, AssignUserCommand> builder)
	{
		var gameName = builder.ClassProperty(r => r.GameName)
			.Required(Errors.Account.AssignUser.GameNameMissing)
			.Map(p => p.GameName, value => new GameName(value));

		var accountName = builder.ClassProperty(r => r.AccountName)
			.Required(Errors.Account.AssignUser.AccountNameMissing)
			.Map(p => p.AccountName, value => new AccountName(value));

		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.Account.AssignUser.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		return builder.Build(() => new AssignUserCommand(gameName, accountName, username));
	}
}

public record AssignUserCommand(
	GameName GameName,
	AccountName AccountName,
	Username Username) : ICommand;
