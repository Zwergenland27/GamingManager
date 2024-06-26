﻿using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.Servers;

public interface IServerRepository
{
	void Add(Server server);

	void Delete(Server server);

	Task<Server?> GetAsync(ServerId id);
	Task<Server?> GetAsync(Hostname hostname);

	Task<bool> HasAnyActiveGameServersAsync(ServerId serverId);

	Task<bool> IsHostnameUniqueAsync(Hostname hostname);

	Task<bool> IsMacUniqueAsync(Mac mac);

	Task<bool> IsAddressUniqueAsync(Uri address);

	Task<bool> HostsGameServer(ServerId serverId);
}
