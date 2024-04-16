using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Domain.Games;

public interface IGameRepository
{
	void Add(Game game);

	void Delete(Game game);

	Task<Game?> GetAsync(GameId id);

	Task<Game?> GetAsync(GameName name);

	Task<bool> IsNameUnique(GameName name);
}
