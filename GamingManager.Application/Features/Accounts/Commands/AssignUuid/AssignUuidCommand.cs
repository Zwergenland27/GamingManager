using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Accounts.Commands.AssignUuid;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Commands.AssignUser;

public class AssignUuidCommandBuilder : IRequestBuilder<AssignUuidParameters, AssignUuidCommand>
{
	public ValidatedRequiredProperty<AssignUuidCommand> Configure(RequiredPropertyBuilder<AssignUuidParameters, AssignUuidCommand> builder)
	{
		var gameName = builder.ClassProperty(r => r.GameName)
			.Required(Errors.Account.AssignUuid.GameNameMissing)
			.Map(p => p.GameName, value => new GameName(value));

		var accountName = builder.ClassProperty(r => r.AccountName)
			.Required(Errors.Account.AssignUuid.AccountNameMissing)
			.Map(p => p.AccountName, value => new AccountName(value));

		var uuid = builder.ClassProperty(r => r.Uuid)
			.Required(Errors.Account.AssignUuid.UuidMissing)
			.Map(p => p.Uuid, value => new Uuid(value));

		return builder.Build(() => new AssignUuidCommand(gameName, accountName, uuid));
	}
}

public record AssignUuidCommand(
	GameName GameName,
	AccountName AccountName,
	Uuid Uuid) : ICommand;
