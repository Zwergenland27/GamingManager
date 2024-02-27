using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Domain.Games;

public interface IGameRepository
{
	Task<Game?> GetAsync(GameId id);

	Task<Game>? GetAsync(GameName Name);
	void Add(Game game);

	void Delete(Game game);

	Task<bool> IsNameUnique(GameName name);
}
