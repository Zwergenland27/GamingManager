using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Projects.Queries.Get;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Queries.Get;

public class GetProjectQueryBuilder : IRequestBuilder<GetProjectParameters, GetProjectQuery>
{
	public ValidatedRequiredProperty<GetProjectQuery> Configure(RequiredPropertyBuilder<GetProjectParameters, GetProjectQuery> builder)
	{
		var projectId = builder.ClassProperty(r => r.ProjectId)
			.Required(Errors.Project.Get.ProjectIdMissing)
			.Map(p => p.ProjectId, ProjectId.Create);

		return builder.Build(() => new GetProjectQuery(projectId));
	}
}

public record GetProjectQuery(ProjectId ProjectId) : IQuery<GetProjectResult>;
