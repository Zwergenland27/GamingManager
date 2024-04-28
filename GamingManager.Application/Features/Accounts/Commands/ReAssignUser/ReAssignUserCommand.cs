using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Accounts.Commands.ReAssignUser;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Commands.ReAssignUser;

public class ReAssignUserCommandBuilder : IRequestBuilder<ReAssignUserParameters, ReAssignUserCommand>
{
	public ValidatedRequiredProperty<ReAssignUserCommand> Configure(RequiredPropertyBuilder<ReAssignUserParameters, ReAssignUserCommand> builder)
	{
		var gameName = builder.ClassProperty(r => r.GameName)
			.Required(Errors.Account.ReAssignUser.GameNameMissing)
			.Map(p => p.GameName, value => new GameName(value));

		var accountName = builder.ClassProperty(r => r.AccountName)
			.Required(Errors.Account.ReAssignUser.AccountNameMissing)
			.Map(p => p.AccountName, value => new AccountName(value));

		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.Account.ReAssignUser.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		return builder.Build(() => new ReAssignUserCommand(gameName, accountName, username));
	}
}

public record ReAssignUserCommand(
	GameName GameName,
	AccountName AccountName,
	Username Username) : ICommand;
