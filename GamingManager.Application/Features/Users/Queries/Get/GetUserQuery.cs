using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Contracts.Features.Users.Queries;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Queries.Get;

public class GetUserQueryBuilder : IRequestBuilder<GetUserParameters, GetUserQuery>
{
	public ValidatedRequiredProperty<GetUserQuery> Configure(RequiredPropertyBuilder<GetUserParameters, GetUserQuery> builder)
	{
		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.Users.Get.UsernameMissing)
			.Map(p => p.CurrentUsername, value => new Username(value));

		return builder.Build(() => new GetUserQuery(username));
	}
}

public record GetUserQuery(Username Username) : IQuery<DetailedUserDto>;
