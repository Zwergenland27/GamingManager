using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public static class GameServerTicket
	{
		public static class Reject
		{
			public static Error IdMissing => Error.Validation(
				"GameServerTicket.Recject.Id.Missing",
				"The id field is required.");

			public static Error ReasonMissing => Error.Validation(
				"GameServerTicket.Recject.Reason.Missing",
				"The reason field is required.");
		}

		public static class Accept
		{
			public static Error IdMissing => Error.Validation(
				"GameServerTicket.Accept.Id.Missing",
				"The id field is required.");

			public static Error GameServerNameMissing => Error.Validation(
				"GameServerTicket.Accept.GameServerName.Missing",
				"The gameServerName field is required.");
		}

		public static class Get
		{
			public static Error IdMissing => Error.Validation(
				"GameServerTicket.Get.Id.Missing",
				"The id field is required.");
		}
	}
}
