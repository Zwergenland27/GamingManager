using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public static class Game
	{
		public static class Create
		{
			public static Error NameMissing => Error.Validation(
				"Games.Create.NameMissing",
				"The name field is required.");

			public static Error VerificationRequiredMissing => Error.Validation(
				"Games.Create.VerificationRequiredMissing",
				"The verificationRequired field is required.");
		}

		public static class Get
		{
			public static Error NameMissing => Error.Validation(
				"Games.Get.NameMissing",
				"The name field is required.");
		}
	}
}
