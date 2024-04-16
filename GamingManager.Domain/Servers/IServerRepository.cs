using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.Servers;

public interface IServerRepository
{
	void Add(Server server);

	void Delete(Server server);

	Task<Server?> GetAsync(ServerId id);
	Task<Server?> GetAsync(Hostname hostname);

	Task<bool> IsHostnameUniqueAsync(Hostname hostname);

	Task<bool> IsMacUniqueAsync(Mac mac);

	Task<bool> IsAddressUniqueAsync(Address address);

	Task<bool> HostsGameServer(ServerId serverId);
}
