using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Servers;

namespace GamingManager.Application.Features.Servers.Commands.Start;

public class StartServerCommandHandler(
	IUnitOfWork unitOfWork,
	IServerRepository serverRepository) : ICommandHandler<StartServerCommand>
{
	public async Task<CanFail> Handle(StartServerCommand request, CancellationToken cancellationToken)
	{
		var server = await serverRepository.GetAsync(request.Hostname);
		if (server is null) return Errors.Servers.HostnameNotFound;

		var result = server.Start();
		if (result.HasFailed) return result.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return result.Errors;
	}
}
