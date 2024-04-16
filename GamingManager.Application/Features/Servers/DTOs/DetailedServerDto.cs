using GamingManager.Domain.Servers;

namespace GamingManager.Application.Features.Servers.DTOs;

public record DetailedServerDto()
{
	public static DetailedServerDto FromServer(Server server)
	{
		return new DetailedServerDto();
	}
}
