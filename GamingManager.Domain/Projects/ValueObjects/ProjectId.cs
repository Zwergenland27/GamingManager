using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Projects.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref="Project"/>
/// </summary>
public sealed record ProjectId(Guid Value)
{
    /// <summary>
    /// Creates new unique <see cref="ProjectId"/>
    /// </summary>
    public static ProjectId CreateNew()
    {
        return new ProjectId(Guid.NewGuid());
    }

    /// <summary>
    /// Creates <see cref="ProjectId"/> instance from <paramref name="value"/>
    /// </summary>
    public static CanFail<ProjectId> Create(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new ProjectId(guid);
        }

        return Errors.Projects.Id.Invalid;
    }
}
