using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Servers;

namespace GamingManager.Application.Features.Servers.Commands.ChangeHostname;

public class ChangeHostnameCommandHandler(
	IUnitOfWork unitOfWork,
	IServerRepository serverRepository) : ICommandHandler<ChangeHostnameCommand>
{
	public async Task<CanFail> Handle(ChangeHostnameCommand request, CancellationToken cancellationToken)
	{
		var server = await serverRepository.GetAsync(request.CurrentHostname);
		if(server is null) return Errors.Servers.HostnameNotFound;

		server.ChangeHostname(request.NewHostname);

		await unitOfWork.SaveAsync(cancellationToken);
		return CanFail.Success();
	}
}
