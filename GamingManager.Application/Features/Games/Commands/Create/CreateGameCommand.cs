using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Games.Commands.Create;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games.Commands.CreateGame;

public class CreateGameCommandBuilder : IRequestBuilder<CreateGameParameters, CreateGameCommand>
{
	public ValidatedRequiredProperty<CreateGameCommand> Configure(RequiredPropertyBuilder<CreateGameParameters, CreateGameCommand> builder)
	{
		var name = builder.ClassProperty(c => c.Name)
			.Required(Errors.Game.Create.NameMissing)
			.Map(r => r.Name, value => new GameName(value));

		var verificationRequired = builder.StructProperty(c => c.VerificationRequired)
			.Required(Errors.Game.Create.VerificationRequiredMissing)
			.Map(r => r.Verificationequired);

		return builder.Build(() => new CreateGameCommand(name, verificationRequired));
	}
}

public record CreateGameCommand(GameName Name, bool VerificationRequired) : ICommand<CreateGameResult>;
