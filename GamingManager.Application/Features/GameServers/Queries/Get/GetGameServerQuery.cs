using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.GameServers.DTOs;
using GamingManager.Contracts.Features.GameServers.Queries;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Queries.Get;

public class GetGameServerQueryBuilder : IRequestBuilder<GetGameServerParameters, GetGameServerQuery>
{
	public ValidatedRequiredProperty<GetGameServerQuery> Configure(RequiredPropertyBuilder<GetGameServerParameters, GetGameServerQuery> builder)
	{
		var name = builder.ClassProperty(r => r.GameServerName)
			.Required(Errors.GameServer.Get.NameMissing)
			.Map(p => p.Name, value => new GameServerName(value));

		return builder.Build(() => new GetGameServerQuery(name));
	}
}

public record GetGameServerQuery(GameServerName GameServerName) : IQuery<DetailedGameServerDto>;
