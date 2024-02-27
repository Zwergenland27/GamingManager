using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Projects.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref=">Organisator"/>
/// </summary>
public readonly record struct TeamMemberId(Guid Value)
{

    /// <summary>
    /// Creates new unique <see cref="TeamMemberId"/>
    /// </summary>
    public static TeamMemberId CreateNew()
    {
        return new TeamMemberId(Guid.NewGuid());
    }

    /// <summary>
    /// Creates <see cref="TeamMemberId"/> instance from <paramref name="value"/>
    /// </summary>
    public static CanFail<TeamMemberId> Create(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new TeamMemberId(guid);
        }

        return Errors.Projects.Team.Id.Invalid;
    }
}
