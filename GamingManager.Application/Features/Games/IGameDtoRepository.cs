using GamingManager.Contracts.Features.Games.Queries.Get;
using GamingManager.Contracts.Features.Games.Queries.GetAll;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games;

public interface IGameDtoRepository
{
	IAsyncEnumerable<GetAllGamesResult> GetAllAsync();

	Task<GetGameResult?> GetAsync(GameName gameName);
	Task<Guid?> GetIdAsync(GameName gameName);
}
