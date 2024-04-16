using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Accounts.Commands.AssignUser;
using GamingManager.Application.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;

namespace GamingManager.Application.Features.Accounts.Commands.AssignUuid;

public class AssignUuidCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IAccountDtoRepository accountDtoRepository,
	IGameRepository gameRepository) : ICommandHandler<AssignUuidCommand, DetailedAccountDto>
{
	public async Task<CanFail<DetailedAccountDto>> Handle(AssignUuidCommand request, CancellationToken cancellationToken)
	{
		var game = await gameRepository.GetAsync(request.GameName);
		if (game is null) return Errors.Games.NameNotFound;

		var account = await accountRepository.GetAsync(game.Id, request.AccountName);
		if (account is null)
		{
			var createResult = Account.Create(game, request.Uuid, request.AccountName);
			if (createResult.HasFailed) return createResult.Errors;

			account = createResult.Value;
		}

		var assignResult = account.AssignUuid(request.Uuid);
		if (assignResult.HasFailed) return assignResult.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return (await accountDtoRepository.GetDetailedAsync(account.Id))!;
	}
}
