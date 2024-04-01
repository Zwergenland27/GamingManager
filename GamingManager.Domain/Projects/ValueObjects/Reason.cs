using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Domain.Projects.ValueObjects;

public sealed record Reason
{
	private Reason(string value)
	{
		Value = value;
	}

	public string Value { get; private init; }

	public static CanFail<Reason> Create(string value)
	{
		if (string.IsNullOrWhiteSpace(value)) return Errors.Projects.Participants.Bans.Reason.Empty;

		return new Reason(value);
	}
}
