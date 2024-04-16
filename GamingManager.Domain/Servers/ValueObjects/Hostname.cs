using CleanDomainValidation.Domain;

namespace GamingManager.Domain.Servers.ValueObjects;

public record Hostname
{
	public Hostname(string value)
	{
		Value = value;
	}

	public string Value { get; private init; }

	public static CanFail<Hostname> Create(string value)
	{
		return new Hostname(value);
	}
}
