using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Servers.Queries.Get;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Queries.Get;

public class GetServerQueryBuilder : IRequestBuilder<GetServerParameters, GetServerQuery>
{
	public ValidatedRequiredProperty<GetServerQuery> Configure(RequiredPropertyBuilder<GetServerParameters, GetServerQuery> builder)
	{
		var hostName = builder.ClassProperty(r => r.Hostname)
			.Required(Errors.Server.Get.HostnameMissing)
			.Map(p => p.Hostname, value => new Hostname(value));

		return builder.Build(() => new GetServerQuery(hostName));
	}
}

public record GetServerQuery(Hostname Hostname) : IQuery<GetServerResult>;
