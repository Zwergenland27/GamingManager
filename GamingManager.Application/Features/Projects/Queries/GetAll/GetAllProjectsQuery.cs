using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Queries.GetAll;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Queries.GetAll;

public class GetAllProjectsQueryBuilder : IRequestBuilder<GetAllProjectsParameters, GetAllProjectsQuery>
{
	public ValidatedRequiredProperty<GetAllProjectsQuery> Configure(RequiredPropertyBuilder<GetAllProjectsParameters, GetAllProjectsQuery> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		return builder.Build(() => new GetAllProjectsQuery(auditorId));
	}
}

public record GetAllProjectsQuery(UserId AuditorId) : IQuery<IEnumerable<GetAllProjectsResult>>;
