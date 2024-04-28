using CleanDomainValidation.Domain;
using GamingManager.Contracts.ContractErrors;

namespace GamingManager.Application.Features.Servers;

public static class Converters
{
	public static CanFail<Uri> CreateUri(string address)
	{
		bool created = Uri.TryCreate(address, UriKind.Absolute, out Uri? uri);
		if (!created) return Errors.Server.Uri.Invalid;

		return uri!;
	}
}
