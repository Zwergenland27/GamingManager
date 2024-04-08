using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;

namespace GamingManager.Application.Features.Games.Commands.CreateGame;

public class CreateGameCommandHandler(
	IUnitOfWork unitOfWork,
	IGameRepository gameRepository) : ICommandHandler<CreateGameCommand, Game>
{
	public async Task<CanFail<Game>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
	{
		var nameUnique = await gameRepository.IsNameUsed(request.Name);
		if (!nameUnique) return Errors.Games.DuplicateName;

		var gameResult = Game.Create(request.Name);
		if (gameResult.HasFailed) return gameResult;

		gameRepository.Add(gameResult.Value);
		await unitOfWork.SaveAsync(cancellationToken);

		return gameResult;
	}
}
