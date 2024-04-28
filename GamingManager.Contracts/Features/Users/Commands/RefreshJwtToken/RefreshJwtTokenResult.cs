using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Users.Commands.RefreshJwtToken;
public class RefreshJwtTokenResult(string Token)
{
	/// <summary>
	/// JWT Token
	/// </summary>
	[Required]
	public string Token { get; init; } = Token;
}
