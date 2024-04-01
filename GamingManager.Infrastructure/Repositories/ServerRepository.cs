using GamingManager.Domain.Servers;
using GamingManager.Domain.Servers.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class ServerRepository(GamingManagerContext context) : IServerRepository
{
	private readonly GamingManagerContext _context = context;

	public void Add(Server server)
	{
		_context.Servers.Add(server);
	}

	public void Delete(Server server)
	{
		_context.Servers.Remove(server);
	}

	public async Task<Server?> GetAsync(ServerId id)
	{
		return await _context.Servers.FirstOrDefaultAsync(server => server.Id == id);
	}
}
