using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Contracts.Features.Games.Queries;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games.Queries.Get;

public class GetGameQueryBuilder : IRequestBuilder<GetGameParameters, GetGameQuery>
{
	public ValidatedRequiredProperty<GetGameQuery> Configure(RequiredPropertyBuilder<GetGameParameters, GetGameQuery> builder)
	{
		var name = builder.ClassProperty(r => r.Name)
			.Required(Errors.Game.Get.NameMissing)
			.Map(p => p.Name, value => new GameName(value));

		return builder.Build(() => new GetGameQuery(name));
	}
}

public record GetGameQuery(GameName Name) : IQuery<DetailedGameDto>;
