using CleanDomainValidation.Domain;

namespace GamingManager.Domain.Servers.ValueObjects;

public class Mac
{
    private Mac(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    public static CanFail<Mac> Create(string value)
    {
        //TODO: implement validation
        return new Mac(value);
    }
}
