using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts;

namespace GamingManager.Application.Features.Accounts;

public static class Converters
{
	public static ShortenedAccountDto ToDto(this Account account)
	{
		return new ShortenedAccountDto(account.Id.Value.ToString(), account.Name.Value);
	}
}
