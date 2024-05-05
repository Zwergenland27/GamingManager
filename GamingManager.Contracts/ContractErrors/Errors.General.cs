using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public static class General
	{
		/// <summary>
		/// The auditor id is required
		/// </summary>
		public static Error AuditorMissing => Error.Validation(
			"General.AuditorMissing",
			"The auditorId is required.");

		public static Error GameServerIdMissing => Error.Validation(
			"General.GameServerIdMissing",
			"The gameServerId is required.");
	}
}
