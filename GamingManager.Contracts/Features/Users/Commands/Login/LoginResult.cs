using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Users.Commands.Login;

public class LoginResult(string Token)
{
	/// <summary>
	/// JWT Token
	/// </summary>
	[Required]
	public string Token { get; init; } = Token;
}
