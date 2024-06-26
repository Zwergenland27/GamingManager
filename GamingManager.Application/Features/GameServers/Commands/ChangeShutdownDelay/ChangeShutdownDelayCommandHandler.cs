﻿using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.GameServers;

namespace GamingManager.Application.Features.GameServers.Commands.ChangeShutdownDelay
{
	public class ChangeShutdownDelayCommandHandler(
		IUnitOfWork unitOfWork,
		IGameServerRepository gameServerRepository) : ICommandHandler<ChangeShutdownDelayCommand>
	{
		public async Task<CanFail> Handle(ChangeShutdownDelayCommand request, CancellationToken cancellationToken)
		{
			var gameServer = await gameServerRepository.GetAsync(request.GameServerName);
			if (gameServer is null) return Errors.GameServers.ServerNameNotFound;

			gameServer.ShutdownDelay = request.ShutdownDelay;

			await unitOfWork.SaveAsync(cancellationToken);

			return CanFail.Success();
		}
	}
}
