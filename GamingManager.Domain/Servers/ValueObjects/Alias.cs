using CleanDomainValidation.Domain;

namespace GamingManager.Domain.Servers.ValueObjects;

public sealed record Alias
{
    private Alias(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    public static CanFail<Alias> Create(string value)
    {
        //TODO: implement validation
        return new Alias(value);
    }
}
