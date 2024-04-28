using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Servers.Queries.Get;
using GamingManager.Domain.DomainErrors;

namespace GamingManager.Application.Features.Servers.Queries.Get;

public class GetServerQueryHandler(
	IServerDtoRepository serverDtoRepository) : IQueryHandler<GetServerQuery, GetServerResult>
{
	public async Task<CanFail<GetServerResult>> Handle(GetServerQuery request, CancellationToken cancellationToken)
	{
		var server = await serverDtoRepository.GetAsync(request.Hostname);
		if (server is null) return Errors.Servers.HostnameNotFound;

		return server;
	}
}
