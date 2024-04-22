using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Games.Commands;
using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games.Commands.CreateGame;

public class CreateGameConfiguration : IRequestBuilder<CreateGameParameters, CreateGameCommand>
{
	public ValidatedRequiredProperty<CreateGameCommand> Configure(RequiredPropertyBuilder<CreateGameParameters, CreateGameCommand> builder)
	{
		var name = builder.ClassProperty(c => c.Name)
			.Required(Errors.Game.Create.NameMissing)
			.Map(r => r.Name, value => new GameName(value));

		return builder.Build(() => new CreateGameCommand(name));
	}
}

public record CreateGameCommand(GameName Name) : ICommand<DetailedGameDto>;
