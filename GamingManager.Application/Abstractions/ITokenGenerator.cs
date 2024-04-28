using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Abstractions;

public interface ITokenGenerator
{
	string GenerateJwtToken(UserId userId, Username username, Role role);

	RefreshToken GenerateRefreshToken();
}
