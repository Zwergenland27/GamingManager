using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Servers.DTOs;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Servers;

namespace GamingManager.Application.Features.Servers.Commands.Create;

public class CreateServerCommandHandler(
	IUnitOfWork unitOfWork,
	IServerRepository serverRepository,
	IServerDtoRepository serverDtoRepository) : ICommandHandler<CreateServerCommand, DetailedServerDto>
{
	public async Task<CanFail<DetailedServerDto>> Handle(CreateServerCommand request, CancellationToken cancellationToken)
	{
		var hostnameUnique = await serverRepository.IsHostnameUniqueAsync(request.Hostname);
		if (!hostnameUnique) return Errors.Servers.DuplicateHostname;

		var addressUnique = await serverRepository.IsAddressUniqueAsync(request.Address);
		if (!addressUnique) return Errors.Servers.DuplicateAddress;

		var macUnique = await serverRepository.IsMacUniqueAsync(request.Mac);
		if (!macUnique) return Errors.Servers.DuplicateMac;

		var serverResult = Server.Create(request.Hostname, request.Address, request.Mac, request.ShutdownDelay);
		if(serverResult.HasFailed) return serverResult.Errors;

		serverRepository.Add(serverResult.Value);

		await unitOfWork.SaveAsync(cancellationToken);

		return (await serverDtoRepository.GetDetailedAsync(request.Hostname))!;
	}
}
