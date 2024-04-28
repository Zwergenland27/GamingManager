using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands.DeleteUser;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.Delete;

public class DeleteUserCommandBuilder : IRequestBuilder<DeleteUserParameters, DeleteUserCommand>
{
	public ValidatedRequiredProperty<DeleteUserCommand> Configure(RequiredPropertyBuilder<DeleteUserParameters, DeleteUserCommand> builder)
	{
		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.User.Delete.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		return builder.Build(() => new DeleteUserCommand(username));
	}
}

public record DeleteUserCommand(Username Username) : ICommand;
