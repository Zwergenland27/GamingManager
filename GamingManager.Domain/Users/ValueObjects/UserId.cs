using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Users.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref="User"/>
/// </summary>
public sealed record UserId(Guid Value)
{
    /// <summary>
    /// Creates new unique <see cref="UserId"/>
    /// </summary>
    public static UserId CreateNew()
    {
        return new UserId(Guid.NewGuid());
    }

    /// <summary>
    /// Creates <see cref="UserId"/> instance from <paramref name="value"/>
    /// </summary>
    public static CanFail<UserId> Create(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new UserId(guid);
        }

        return Errors.Users.Id.Invalid;
    }
}
