using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Projects.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref=">Ban"/>
/// </summary>
public sealed record BanId(Guid Value)
{
	/// <summary>
	/// Creates new unique <see cref="BanId"/>
	/// </summary>
	public static BanId CreateNew()
	{
		return new BanId(Guid.NewGuid());
	}

	/// <summary>
	/// Creates <see cref="BanId"/> instance from <paramref name="value"/>
	/// </summary>
	public static CanFail<BanId> Create(string value)
	{
		if (Guid.TryParse(value, out var guid))
		{
			return new BanId(guid);
		}

		return Errors.Projects.Participants.Bans.Id.Invalid;
	}
}
