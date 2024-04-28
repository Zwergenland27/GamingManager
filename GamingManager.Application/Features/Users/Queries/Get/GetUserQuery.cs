using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Queries.Get;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Queries.Get;

public class GetUserQueryBuilder : IRequestBuilder<GetUserParameters, GetUserQuery>
{
	public ValidatedRequiredProperty<GetUserQuery> Configure(RequiredPropertyBuilder<GetUserParameters, GetUserQuery> builder)
	{
		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.User.Get.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		return builder.Build(() => new GetUserQuery(username));
	}
}

public record GetUserQuery(Username Username) : IQuery<GetUserResult>;
