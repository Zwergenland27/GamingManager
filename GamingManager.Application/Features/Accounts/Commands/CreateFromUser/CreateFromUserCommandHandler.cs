using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Accounts.Commands.CreateFromLogin;

public class CreateFromUserCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IAccountDtoRepository accountDtoRepository,
	IGameRepository gameRepository,
	IUserRepository userRepository) : ICommandHandler<CreateFromUserCommand, DetailedAccountDto>
{
	public async Task<CanFail<DetailedAccountDto>> Handle(CreateFromUserCommand request, CancellationToken cancellationToken)
	{
		var game = await gameRepository.GetAsync(request.GameName);
		if (game is null) return Errors.Games.NameNotFound;

		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		var existing = await accountRepository.GetAsync(game.Id, request.AccountName);
		if (existing is not null) return Errors.Accounts.DuplicateName;

		var accountResult = Account.Create(game, user, request.AccountName);
		if(accountResult.HasFailed) return accountResult.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return (await accountDtoRepository.GetDetailedAsync(accountResult.Value.Id))!;
	}
}
