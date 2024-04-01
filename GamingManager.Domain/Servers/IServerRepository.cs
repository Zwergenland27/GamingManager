using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.Servers;

public interface IServerRepository
{
	void Add(Server server);

	void Delete(Server server);

	Task<Server?> GetAsync(ServerId id);
}
