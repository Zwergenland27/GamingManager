using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Accounts.Commands.AssignUser;

public class AssignUserCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IAccountDtoRepository accountDtoRepository,
	IGameRepository gameRepository,
	IUserRepository userRepository) : ICommandHandler<AssignUserCommand, DetailedAccountDto>
{
	public async Task<CanFail<DetailedAccountDto>> Handle(AssignUserCommand request, CancellationToken cancellationToken)
	{
		var game = await gameRepository.GetAsync(request.GameName);
		if (game is null) return Errors.Games.NameNotFound;

		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		var account = await accountRepository.GetAsync(game.Id, request.AccountName);
		if (account is null) return Errors.Accounts.NameNotFound;

		var assignResult = account.AssignUser(user);
		if (assignResult.HasFailed) return assignResult.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return (await accountDtoRepository.GetDetailedAsync(account.Id))!;
	}
}
