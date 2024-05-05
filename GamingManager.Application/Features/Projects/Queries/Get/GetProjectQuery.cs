using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Queries.Get;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Projects.Queries.Get;

public class GetProjectQueryBuilder : IRequestBuilder<GetProjectParameters, GetProjectQuery>
{
	public ValidatedRequiredProperty<GetProjectQuery> Configure(RequiredPropertyBuilder<GetProjectParameters, GetProjectQuery> builder)
	{
		var auditorId = builder.ClassProperty(r => r.AuditorId)
			.Required(Errors.General.AuditorMissing)
			.Map(p => p.AuditorId, UserId.Create);

		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.Get.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		return builder.Build(() => new GetProjectQuery(auditorId, projectId));
	}
}

public record GetProjectQuery(
	UserId AuditorId,
	ProjectId ProjectId) : IQuery<GetProjectResult>;
