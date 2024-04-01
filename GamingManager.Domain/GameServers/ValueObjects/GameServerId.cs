using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Servers;

namespace GamingManager.Domain.GameServers.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref=">GameServer"/>
/// </summary>
public sealed record GameServerId(Guid Value)
{
    /// <summary>
    /// Creates new unique <see cref="GameServerId"/>
    /// </summary>
    public static GameServerId CreateNew()
    {
        return new GameServerId(Guid.NewGuid());
    }

    /// <summary>
    /// Creates <see cref="GameServerId"/> instance from <paramref name="value"/>
    /// </summary>
    public static CanFail<GameServerId> Create(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new GameServerId(guid);
        }

        return Errors.GameServers.Id.Invalid(value);
    }
}
