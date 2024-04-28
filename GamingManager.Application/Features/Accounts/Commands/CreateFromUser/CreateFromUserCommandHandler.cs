using CleanDomainValidation.Domain;
using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Accounts.Commands.CreateFromUser;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;
using GamingManager.Domain.Users;

namespace GamingManager.Application.Features.Accounts.Commands.CreateFromLogin;

public class CreateFromUserCommandHandler(
	IUnitOfWork unitOfWork,
	IAccountRepository accountRepository,
	IGameRepository gameRepository,
	IUserRepository userRepository) : ICommandHandler<CreateFromUserCommand, CreateFromUserResult>
{
	public async Task<CanFail<CreateFromUserResult>> Handle(CreateFromUserCommand request, CancellationToken cancellationToken)
	{
		var game = await gameRepository.GetAsync(request.GameName);
		if (game is null) return Errors.Games.NameNotFound;

		var user = await userRepository.GetAsync(request.Username);
		if (user is null) return Errors.Users.UsernameNotFound;

		var existing = await accountRepository.GetAsync(game.Id, request.AccountName);
		if (existing is not null) return Errors.Accounts.DuplicateName;

		var accountResult = Account.Create(game, user, request.AccountName);
		if (accountResult.HasFailed) return accountResult.Errors;

		await unitOfWork.SaveAsync(cancellationToken);

		return new CreateFromUserResult(
			Id: accountResult.Value.Id.Value.ToString(),
			Name: accountResult.Value.Name.Value,
			Uuid: accountResult.Value.Uuid?.Value,
			User: new CreateFromUserUserResult(
				Id: user.Id.Value.ToString(),
				Username: user.Username.Value,
				accountResult.Value.IsConfirmed),
			Game: new CreateFromUserGameResult(
				Id: game.Id.Value.ToString(),
				Name: game.Name.Value));
	}
}
