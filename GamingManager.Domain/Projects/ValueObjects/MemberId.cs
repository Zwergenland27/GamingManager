using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Projects.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref="TeamMember"/>"/>
/// </summary>
public sealed record MemberId(Guid Value)
{

    /// <summary>
    /// Creates new unique <see cref="MemberId"/>
    /// </summary>
    public static MemberId CreateNew()
    {
        return new MemberId(Guid.NewGuid());
    }

    /// <summary>
    /// Creates <see cref="MemberId"/> instance from <paramref name="value"/>
    /// </summary>
    public static CanFail<MemberId> Create(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new MemberId(guid);
        }

        return Errors.Projects.Members.Id.Invalid;
    }
}
