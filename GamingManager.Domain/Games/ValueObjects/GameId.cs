using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Games.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref="Game"/>
/// </summary>
public readonly record struct GameId(Guid Value)
{
    /// <summary>
    /// Creates new unique <see cref="GameId"/>
    /// </summary>
    public static GameId CreateNew()
    {
        return new GameId(Guid.NewGuid());
    }

    /// <summary>
    /// Creates <see cref="GameId"/> instance from <paramref name="value"/>
    /// </summary>
    public static CanFail<GameId> Create(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new GameId(guid);
        }

        return Errors.Games.Id.Invalid;
    }
}
