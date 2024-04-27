using GamingManager.Application.Abstractions;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GamingManager.Infrastructure.Authentification;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
{
	private readonly JwtSettings jwtSettings = jwtOptions.Value;
	public string GenerateToken(UserId userId, Username username, Role role)
	{
		List<Claim> claims = [
			new Claim(ClaimTypes.NameIdentifier, userId.Value.ToString()),
			new Claim(ClaimTypes.Name, username.Value),
			new Claim(ClaimTypes.Role, role.ToString())
			];

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
			issuer: jwtSettings.Issuer,
			audience: jwtSettings.Audience,
			expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationInMinutes),
			claims: claims,
			signingCredentials: credentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
