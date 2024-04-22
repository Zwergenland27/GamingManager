using CleanDomainValidation.Domain;

namespace GamingManager.Domain.Servers.ValueObjects;

public sealed record Address
{
    private Address(string ip)
    {
		Ip = ip;
    }

    public string Ip { get; private init; }

    public static CanFail<Address> Create(string ip)
    {
        //TODO: implement validation

        return new Address(ip);
    }
}
