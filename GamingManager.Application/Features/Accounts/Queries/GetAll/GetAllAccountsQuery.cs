using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Accounts.Queries.GetAllOfGame;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Queries.GetAll;

public class GetAllAccountsQueryBuilder : IRequestBuilder<GetAllAccountsParameters, GetAllAccountsQuery>
{
	public ValidatedRequiredProperty<GetAllAccountsQuery> Configure(RequiredPropertyBuilder<GetAllAccountsParameters, GetAllAccountsQuery> builder)
	{
		var gameName = builder.ClassProperty(r => r.GameName)
			.Required(Errors.Account.GetAll.GameNameMissing)
			.Map(p => p.GameName, value => new GameName(value));

		return builder.Build(() => new GetAllAccountsQuery(gameName));
	}
}

public record GetAllAccountsQuery(GameName GameName) : IQuery<IEnumerable<GetAllAccountsResult>>;
