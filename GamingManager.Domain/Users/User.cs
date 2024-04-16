using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Domain.Users;

/// <summary>
/// User of the software
/// </summary>
public class User : AggregateRoot<UserId>
{
	private User(
		Firstname? firstname,
		Lastname? lastname,
		Email email,
		Username username,
		Role role) : base(UserId.CreateNew())
	{
		Firstname = firstname;
		Lastname = lastname;
		Email = email;
		Username = username;
		Role = role;
	}

#pragma warning disable CS8618
	private User() : base(default!) { }
#pragma warning restore CS8618

	public Firstname? Firstname { get; set; }

	public Lastname? Lastname { get; set; }

	public Email Email { get; set; }

	public Username Username { get; set; }

	public Role Role { get; set; }

	/// <summary>
	/// Creates new <see cref="User"/> instance
	/// </summary>
	public static CanFail<User> Create(
		Firstname? firstname,
		Lastname? lastname,
		Email email,
		Username username)
	{
		return new User(firstname, lastname, email, username, Role.Guest);
	}
}

