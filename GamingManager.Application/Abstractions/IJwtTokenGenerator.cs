using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Abstractions;

public interface IJwtTokenGenerator
{
	string GenerateToken(UserId userId, Username username, Role role);
}
