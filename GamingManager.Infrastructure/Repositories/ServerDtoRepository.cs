using GamingManager.Application.Features.Servers;
using GamingManager.Contracts.Features.Servers.Queries.Get;
using GamingManager.Contracts.Features.Servers.Queries.GetAll;
using GamingManager.Domain.Servers.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class ServerDtoRepository(GamingManagerReadContext context) : IServerDtoRepository
{
	public IAsyncEnumerable<GetAllServersResult> GetAllAsync()
	{
		return context.Servers
			.Select(server => new GetAllServersResult(
				server.Id.ToString(),
				server.Hostname,
				server.Status.ToString(),
				server.Mac,
				server.Address))
			.AsAsyncEnumerable();
	}

	public async Task<GetServerResult?> GetAsync(Hostname hostname)
	{
		return await context.Servers
			.Include(server => server.Hosts)
			.Where(server => server.Hostname == hostname.Value)
			.Select(server => new GetServerResult(
				server.Id.ToString(),
				server.Hostname,
				server.Status.ToString(),
				server.Mac,
				server.Address,
				server.Hosts.Select(gameServer => new GetServerGameServersResult(
					gameServer.Id.ToString(),
					gameServer.ServerName,
					gameServer.Status.ToString()))
				.ToList()))
			.FirstOrDefaultAsync();
	}
}
