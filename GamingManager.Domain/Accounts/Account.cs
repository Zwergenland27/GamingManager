using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Accounts.Events;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Users;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Accounts;

public class Account : AggregateRoot<AccountId>
{
	private Account(
		GameId gameId,
		Uuid? uuid,
		AccountName name) : base(new AccountId(Guid.NewGuid()))
	{
		Game = gameId;
		Uuid = uuid;
		Name = name;
	}
#pragma warning disable CS8618
	private Account() : base(default!) { }
#pragma warning restore CS8618

	public UserId? User { get; private set; }
	public GameId Game { get; private init; }
	public Uuid? Uuid { get; private set; }
	public AccountName Name { get; internal set; }
	public bool IsConfirmed => User is not null && Uuid is not null;

	internal static CanFail<Account> Create(Game game, Uuid uuid, AccountName name)
	{
		var account = new Account(game.Id, uuid, name);
		account.RaiseDomainEvent(new AccountCreatedEvent(account.Id, null));
		return account;
	}

	internal static CanFail<Account> Create(Game game, User user, AccountName name)
	{
		var account = new Account(game.Id, null, name);
		var assignResult = account.AssignUser(user);
		if (assignResult.HasFailed) return CanFail<Account>.FromFailure(assignResult);
		account.RaiseDomainEvent(new AccountCreatedEvent(account.Id, user.Id));
		return account;
	}

	public CanFail AssignUser(User user)
	{
		if (User is not null) return Errors.Accounts.User.AlreadyAssigned;
		User = user.Id;
		RaiseDomainEvent(new AccountUserAssignedEvent(Id, user.Id));
		return CanFail.Success();
	}

	public CanFail AssignUuid(Uuid uuid)
	{
		if (Uuid is not null) return Errors.Accounts.Uuid.AlreadyAssigned;
		Uuid = uuid;
		return CanFail.Success();
	}

	public void ForceReAssignUser(User user)
	{
		User = user.Id;
		RaiseDomainEvent(new AccountUserAssignedEvent(Id, user.Id));
	}
}
