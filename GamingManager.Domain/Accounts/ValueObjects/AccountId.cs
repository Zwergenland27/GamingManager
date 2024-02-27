using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Accounts.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref="Account"/>
/// </summary>
public readonly record struct AccountId(Guid Value)
{
	/// <summary>
	/// Creates new unique <see cref="AccountId"/>
	/// </summary>
	public static AccountId CreateNew()
	{
		return new AccountId(Guid.NewGuid());
	}

	/// <summary>
	/// Creates <see cref="AccountId"/> instance from <paramref name="value"/>
	/// </summary>
	public static CanFail<AccountId> Create(string value)
	{
		if (Guid.TryParse(value, out var guid))
		{
			return new AccountId(guid);
		}

		return Errors.Accounts.Id.Invalid;
	}
}
