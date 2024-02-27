using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Projects.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref=">Session"/>
/// </summary>
public readonly record struct SessionId(Guid Value)
{
    /// <summary>
    /// Creates new unique <see cref="SessionId"/>
    /// </summary>
    public static SessionId CreateNew()
    {
        return new SessionId(Guid.NewGuid());
    }

    /// <summary>
    /// Creates <see cref="SessionId"/> instance from <paramref name="value"/>
    /// </summary>
    public static CanFail<SessionId> Create(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new SessionId(guid);
        }

        return Errors.Projects.Participants.Sessions.Id.Invalid;
    }
}
