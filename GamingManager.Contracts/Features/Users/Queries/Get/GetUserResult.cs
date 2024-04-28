using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Users.Queries.Get;

public class GetUserResult(
	string Id,
	string Username,
	string Email,
	string Role,
	string? Firstname,
	string? Lastname,
	bool EmailVerified,
	IReadOnlyCollection<GetUserAccountResult> Accounts,
	IReadOnlyCollection<GetUserProjectsResult> Projects) : UserCoreResult(Id, Username)
{
	/// <summary>
	/// First name of the user
	/// </summary>
	/// <example>Max</example>
	public string? Firstname { get; init; } = Firstname;

	/// <summary>
	/// Last name of the user
	/// </summary>
	/// <example>Mustermann</example>
	public string? Lastname { get; init; } = Lastname;

	/// <summary>
	/// Email of the user
	/// </summary>
	/// <example>zwergenland27@gmail.com</example>
	[Required]
	public string Email { get; init; } = Email;

	/// <summary>
	/// The role of the user
	/// </summary>
	/// <example>Guest</example>
	[Required]
	public string Role { get; init; } = Role;

	/// <summary>
	/// Is the email of the user verified
	/// </summary>
	[Required]
	public bool EmailVerified { get; init; } = EmailVerified;

	/// <summary>
	/// The accounts of the user
	/// </summary>
	[Required]
	public IReadOnlyCollection<GetUserAccountResult> Accounts { get; init; } = Accounts;

	/// <summary>
	/// Projects the user is managing or participating in
	/// </summary>
	[Required]
	public IReadOnlyCollection<GetUserProjectsResult> Projects { get; init; } = Projects;
}
