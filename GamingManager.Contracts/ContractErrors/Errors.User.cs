using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public static class Users
	{
		public static class Create
		{
			public static Error UsernameMissing => Error.Validation(
				"Users.Create.UsernameMissing",
				"Username field is missing");

			public static Error EmailMissing => Error.Validation(
				"Users.Create.EmailMissing",
				"Email field is missing");
		}
	}
}
