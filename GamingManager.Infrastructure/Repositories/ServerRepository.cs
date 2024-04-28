using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Servers.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class ServerRepository(GamingManagerDomainContext context) : IServerRepository
{
	public void Add(Server server)
	{
		context.Servers.Add(server);
	}

	public void Delete(Server server)
	{
		context.Servers.Remove(server);
	}

	public async Task<Server?> GetAsync(ServerId id)
	{
		return await context.Servers.FirstOrDefaultAsync(server => server.Id == id);
	}

	public async Task<Server?> GetAsync(Hostname hostname)
	{
		return await context.Servers.FirstOrDefaultAsync(server => server.Hostname == hostname);
	}

	public async Task<bool> HasAnyActiveGameServersAsync(ServerId serverId)
	{
		return !await context.GameServers.AnyAsync(gameServer => gameServer.HostedOnId == serverId && gameServer.Status != GameServerStatus.Offline);
	}

	public async Task<bool> HostsGameServer(ServerId serverId)
	{
		return await context.GameServers.AnyAsync(gameServer => gameServer.HostedOnId == serverId);
	}

	public async Task<bool> IsAddressUniqueAsync(Uri address)
	{
		return !await context.Servers.AnyAsync(server => server.Address == address);
	}

	public async Task<bool> IsHostnameUniqueAsync(Hostname hostname)
	{
		return !await context.Servers.AnyAsync(server => server.Hostname == hostname);
	}

	public async Task<bool> IsMacUniqueAsync(Mac mac)
	{
		return !await context.Servers.AnyAsync(server => server.Mac == mac);
	}
}
