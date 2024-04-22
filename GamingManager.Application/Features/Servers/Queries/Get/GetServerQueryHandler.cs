using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Servers.DTOs;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Servers.Queries.Get;

public class GetServerQueryHandler(
	IServerDtoRepository serverDtoRepository) : IQueryHandler<GetServerQuery, DetailedServerDto>
{
	public async Task<CanFail<DetailedServerDto>> Handle(GetServerQuery request, CancellationToken cancellationToken)
	{
		var server = await serverDtoRepository.GetDetailedAsync(request.Hostname);
		if (server is null) return Errors.Servers.HostnameNotFound;

		return server;
	}
}
