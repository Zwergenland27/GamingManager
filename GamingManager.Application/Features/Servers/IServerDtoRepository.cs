using GamingManager.Application.Features.Servers.DTOs;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers;

public interface IServerDtoRepository
{
	Task<DetailedServerDto> GetAsync(Hostname hostname);
}
