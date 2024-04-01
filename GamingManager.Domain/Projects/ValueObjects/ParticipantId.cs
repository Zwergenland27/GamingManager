using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Projects.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref=">Participant"/>
/// </summary>
public sealed record ParticipantId(Guid Value)
{
    /// <summary>
    /// Creates new unique <see cref="ParticipantId"/>
    /// </summary>
    public static ParticipantId CreateNew()
    {
        return new ParticipantId(Guid.NewGuid());
    }

    /// <summary>
    /// Creates <see cref="ParticipantId"/> instance from <paramref name="value"/>
    /// </summary>
    public static CanFail<ParticipantId> Create(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new ParticipantId(guid);
        }

        return Errors.Projects.Participants.Id.Invalid;
    }
}
