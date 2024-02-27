using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Servers.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref=">Server"/>
/// </summary>
public readonly record struct ServerId(Guid Value)
{
	/// <summary>
	/// Creates new unique <see cref="ServerId"/>
	/// </summary>
	public static ServerId CreateNew()
	{
		return new ServerId(Guid.NewGuid());
	}

	/// <summary>
	/// Creates <see cref="ServerId"/> instance from <paramref name="value"/>
	/// </summary>
	public static CanFail<ServerId> Create(string value)
	{
		if (Guid.TryParse(value, out var guid))
		{
			return new ServerId(guid);
		}

		return Errors.Servers.Id.Invalid;
	}
}
