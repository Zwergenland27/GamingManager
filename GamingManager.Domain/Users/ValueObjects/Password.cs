using CleanDomainValidation.Domain;

namespace GamingManager.Domain.Users.ValueObjects;

public record Password
{
	private Password(string value)
	{
		Value = value;
	}
	public string Value { get; }

	public static CanFail<Password> Create(string value)
	{
		return new Password(value);
	}
}
