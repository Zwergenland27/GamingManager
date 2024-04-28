using System.Security.Cryptography;

namespace GamingManager.Domain.Users.ValueObjects;

public class EmailVerificationToken
{
	public string Token { get; private set; } = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)).Replace('/', 'a').Replace('?', 'q');
	public DateTime Created { get; private set; } = DateTime.UtcNow;
	public DateTime Expires { get; private set; } = DateTime.UtcNow.AddHours(1);

	public bool IsValid(string token) => Token == token && DateTime.UtcNow <= Expires;
}
