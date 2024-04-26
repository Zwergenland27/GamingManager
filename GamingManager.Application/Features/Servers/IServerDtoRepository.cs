using GamingManager.Contracts.Features.Servers.DTOs;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers;

public interface IServerDtoRepository
{
	IAsyncEnumerable<ShortenedServerDto> GetAllAsync();
	Task<DetailedServerDto?> GetDetailedAsync(Hostname hostname);
}
