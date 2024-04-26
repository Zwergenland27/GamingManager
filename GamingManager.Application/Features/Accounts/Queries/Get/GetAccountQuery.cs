using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Contracts.Features.Accounts.Queries;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Queries.Get;

public class GetAccountQueryBuilder : IRequestBuilder<GetAccountParameters, GetAccountQuery>
{
	public ValidatedRequiredProperty<GetAccountQuery> Configure(RequiredPropertyBuilder<GetAccountParameters, GetAccountQuery> builder)
	{
		var gameName = builder.ClassProperty(r => r.GameName)
			.Required(Errors.Account.Get.GameNameMissing)
			.Map(p => p.GameName, value => new GameName(value));

		var accountName = builder.ClassProperty(r => r.AccountName)
			.Required(Errors.Account.Get.AccountNameMissing)
			.Map(p => p.AccountName, value => new AccountName(value));

		return builder.Build(() => new GetAccountQuery(gameName, accountName));
	}
}

public record GetAccountQuery(
	GameName GameName,
	AccountName AccountName) : IQuery<DetailedAccountDto>;
