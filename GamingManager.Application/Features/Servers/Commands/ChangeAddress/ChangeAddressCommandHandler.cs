using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Servers;

namespace GamingManager.Application.Features.Servers.Commands.ChangeAddress;

public class ChangeAddressCommandHandler(
	IUnitOfWork unitOfWork,
	IServerRepository serverRepository) : ICommandHandler<ChangeAddressCommand>
{
	public async Task<CanFail> Handle(ChangeAddressCommand request, CancellationToken cancellationToken)
	{
		var server = await serverRepository.GetAsync(request.Hostname);
		if(server is null) return Errors.Servers.HostnameNotFound;

		server.ChangeAddress(request.Address);

		await unitOfWork.SaveAsync(cancellationToken);

		return CanFail.Success();
	}
}
