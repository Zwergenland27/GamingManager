using GamingManager.Contracts.Features.Projects.DTOs;
using GamingManager.Contracts.Features.Users.DTOs;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Application.Features.Projects.DTOs;

public record DetailedMemberDto(
	string Id,
	string Role,
	DateTime MemberSince,
	ShortenedUserDto User)
{
	///<summary>
	/// Unique id of the member
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[Required]
	public string Id { get; init; } = Id;

	/// <summary>
	/// The role of the member
	/// </summary>
	/// <example>Admin</example>
	[Required]
	public string Role { get; init; } = Role;

	/// <summary>
	/// Since when the member is part of the project team
	/// </summary>
	[Required]
	public DateTime MemberSince { get; init; } = MemberSince;

	/// <summary>
	/// The user information of the member
	/// </summary>
	[Required]
	public ShortenedUserDto User { get; init; } = User;
}
