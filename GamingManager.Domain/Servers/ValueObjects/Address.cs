using CleanDomainValidation.Domain;

namespace GamingManager.Domain.Servers.ValueObjects;

public sealed record Address
{
    private Address(string ip, int port)
    {
		Ip = ip;
        Port = port;
    }

    public string Ip { get; private init; }

    public int Port { get; private init; }

    public static CanFail<Address> Create(string ip, int? port)
    {
        //TODO: implement validation

        if(port is null)
        {
            port = 443;
        }

        return new Address(ip, port.Value);
    }
}
