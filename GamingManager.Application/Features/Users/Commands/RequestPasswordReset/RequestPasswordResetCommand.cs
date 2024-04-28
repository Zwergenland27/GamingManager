using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands.RequestPasswordReset;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.RequestPasswordReset;

public class RequestPasswordResetCommandBuilder : IRequestBuilder<RequestPasswordResetParameters, RequestPasswordResetCommand>
{
	public ValidatedRequiredProperty<RequestPasswordResetCommand> Configure(RequiredPropertyBuilder<RequestPasswordResetParameters, RequestPasswordResetCommand> builder)
	{
		var username = builder.ClassProperty(r => r.Email)
			.Required(Errors.User.RequestPasswordReset.EmailMissing)
			.Map(p => p.Email, Email.Create);

		return builder.Build(() => new RequestPasswordResetCommand(username));
	}
}

public record RequestPasswordResetCommand(Email Email) : ICommand;