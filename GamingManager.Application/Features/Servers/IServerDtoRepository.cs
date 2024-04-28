using GamingManager.Contracts.Features.Servers.Queries.Get;
using GamingManager.Contracts.Features.Servers.Queries.GetAll;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers;

public interface IServerDtoRepository
{
	IAsyncEnumerable<GetAllServersResult> GetAllAsync();
	Task<GetServerResult?> GetAsync(Hostname hostname);
}
