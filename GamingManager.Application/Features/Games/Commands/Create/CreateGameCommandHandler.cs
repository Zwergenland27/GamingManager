using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Games.DTOs;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;

namespace GamingManager.Application.Features.Games.Commands.CreateGame;

public class CreateGameCommandHandler(
	IUnitOfWork unitOfWork,
	IGameRepository gameRepository) : ICommandHandler<CreateGameCommand, DetailedGameDto>
{
	public async Task<CanFail<DetailedGameDto>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
	{
		var nameUnique = await gameRepository.IsNameUnique(request.Name);
		if (!nameUnique) return Errors.Games.DuplicateName;

		var gameResult = Game.Create(request.Name);
		if (gameResult.HasFailed) return gameResult.Errors;

		gameRepository.Add(gameResult.Value);
		await unitOfWork.SaveAsync(cancellationToken);

		return DetailedGameDto.FromGame(gameResult.Value);
	}
}
