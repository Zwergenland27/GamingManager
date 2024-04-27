using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Users.Events;
using GamingManager.Domain.Users.ValueObjects;
using System.Security.Cryptography;

namespace GamingManager.Domain.Users;

/// <summary>
/// User of the software
/// </summary>
public class User : AggregateRoot<UserId>
{
	private string _passwordHash;
	private string _passwordSalt;

	private string? _emailVerificationToken;
	private DateTime? _emailVerificationRequested;


	private User(
		Firstname? firstname,
		Lastname? lastname,
		Email email,
		Username username,
		Role role,
		string passwordHash,
		string passwordSalt) : base(UserId.CreateNew())
	{
		Firstname = firstname;
		Lastname = lastname;
		Email = email;
		Username = username;
		Role = role;
		_passwordHash = passwordHash;
		_passwordSalt = passwordSalt;
	}

#pragma warning disable CS8618
	private User() : base(default!) { }
#pragma warning restore CS8618

	public Firstname? Firstname { get; set; }

	public Lastname? Lastname { get; set; }

	public Email Email { get; set; }

	public bool EmailConfirmed { get; private set; }

	public Username Username { get; set; }

	public Role Role { get; set; }

	/// <summary>
	/// Creates new <see cref="User"/> instance
	/// </summary>
	public static CanFail<User> Create(
		Firstname? firstname,
		Lastname? lastname,
		Email email,
		Username username,
		Password password)
	{
		string passwordHash;
		string passwordSalt;
		using (var hmac = new HMACSHA256())
		{
			passwordSalt = Convert.ToBase64String(hmac.Key);
			passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password.Value)));
		}

		var user = new User(firstname, lastname, email, username, Role.Guest, passwordHash, passwordSalt);

		user.RaiseDomainEvent(new UserCreatedEvent(user.Username, user.Email));

		return user;
	}

	public bool IsPasswordCorret(Password password)
	{
		using (var hmac = new HMACSHA256(Convert.FromBase64String(_passwordSalt)))
		{
			var computedHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password.Value)));
			return computedHash == _passwordHash;
		}
	}

	public void RequestPasswordReset()
	{
		var newPassword = Password.Create(Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))).Value;
		ChangePassword(newPassword);
		RaiseDomainEvent(new PasswordResettedEvent(Username, Email, newPassword));
	}

	public void RequestEmailVerification()
	{
		_emailVerificationToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)).Replace('/', 'a').Replace('?', 'q');
		_emailVerificationRequested = DateTime.UtcNow;
		RaiseDomainEvent(new EmailVerificationRequestedEvent(Username, Email, _emailVerificationToken));
	}

	public CanFail ConfirmEmail(string token)
	{
        if (_emailVerificationRequested is null) return Errors.Users.InvalidEmailVerification;
		if (_emailVerificationRequested.Value.AddHours(24) < DateTime.UtcNow) return Errors.Users.InvalidEmailVerification;
		if (_emailVerificationToken != token) return Errors.Users.InvalidEmailVerification;

		EmailConfirmed = true;
		_emailVerificationToken = null;
		_emailVerificationRequested = null;
		return CanFail.Success();
	}

	public void ChangePassword(Password password)
	{
		using (var hmac = new HMACSHA256())
		{
			_passwordSalt = Convert.ToBase64String(hmac.Key);
			_passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password.Value)));
		}
	}

	public void Delete()
	{
		Username = new Username($"DeletedUser{Id}");
		Email = Email.Create($"{Id}@deleted.de").Value;
		Role = Role.Deleted;
	}

	public CanFail EditPersonalData(
		Username? username,
		Firstname? firstname,
		Lastname? lastname,
		Email? email)
	{
		bool hasChanges = false;

		if (username is not null)
		{
			Username = username;
			hasChanges = true;
		}

		if (firstname is not null)
		{
			Firstname = firstname;
			hasChanges = true;
		}

		if (lastname is not null)
		{
			Lastname = lastname;
			hasChanges = true;
		}

		if (email is not null)
		{
			Email = email;
			hasChanges = true;
		}

		if (!hasChanges) return Errors.Users.NoChanges;

		return CanFail.Success();
	}
}

