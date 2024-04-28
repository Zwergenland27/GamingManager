using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Users.Commands.RefreshJwtToken;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Commands.RefreshJwtToken;

public class RefreshJwtTokenCommandBuilder : IRequestBuilder<RefreshJwtTokenParameters, RefreshJwtTokenCommand>
{
	public ValidatedRequiredProperty<RefreshJwtTokenCommand> Configure(RequiredPropertyBuilder<RefreshJwtTokenParameters, RefreshJwtTokenCommand> builder)
	{
		var username = builder.ClassProperty(r => r.Username)
			.Required(Errors.User.RefreshJwtToken.UsernameMissing)
			.Map(p => p.Username, value => new Username(value));

		var refreshToken = builder.ClassProperty(r => r.RefreshToken)
			.Required(Errors.User.RefreshJwtToken.RefreshTokenMissing)
			.Map(p => p.RefreshToken);

		return builder.Build(() => new RefreshJwtTokenCommand(username, refreshToken));
	}
}

public record RefreshJwtTokenCommand(Username Username, string RefreshToken) : ICommand<Tuple<string, RefreshToken>>;
