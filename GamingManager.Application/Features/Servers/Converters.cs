using CleanDomainValidation.Domain;
using GamingManager.Contracts.ContractErrors;
using GamingManager.Contracts.Features.Servers.DTOs;
using GamingManager.Domain.Servers;

namespace GamingManager.Application.Features.Servers;

public static class Converters
{
	public static CanFail<Uri> CreateUri(string address)
	{
		bool created = Uri.TryCreate(address, UriKind.Absolute, out Uri? uri);
		if (!created) return Errors.Server.Uri.Invalid;

		return uri!;
	}

	public static ShortenedServerDto ToDto(this Server server)
	{
		return new ShortenedServerDto(
			server.Id.Value.ToString(),
			server.Hostname.Value);
	}
}
