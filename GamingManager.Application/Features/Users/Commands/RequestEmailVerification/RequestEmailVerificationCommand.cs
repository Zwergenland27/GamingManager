using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands.RequestEmailVerification;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.RequestEmailVerification;

public class RequestEmailVerificationCommandBuilder : IRequestBuilder<RequestEmailVerificationParameters, RequestEmailVerificationCommand>
{
	public ValidatedRequiredProperty<RequestEmailVerificationCommand> Configure(RequiredPropertyBuilder<RequestEmailVerificationParameters, RequestEmailVerificationCommand> builder)
	{
		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.User.RequestEmailVerification.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		return builder.Build(() => new RequestEmailVerificationCommand(username));
	}
}

public record RequestEmailVerificationCommand(Username Username) : ICommand;
