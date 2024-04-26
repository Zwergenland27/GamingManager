using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public static class Users
	{
		public static class Create
		{
			public static Error UsernameMissing => Error.Validation(
				"User.Create.UsernameMissing",
				"The username field is required.");

			public static Error EmailMissing => Error.Validation(
				"User.Create.EmailMissing",
				"The email field is required.");
		}

		public static class EditPersonalData
		{
			public static Error CurrentUsernameMissing => Error.Validation(
				"User.EditPersonalData.CurrentUsernameMissing",
				"The username field is required.");
		}

		public static class Delete
		{
			public static Error UsernameMissing => Error.Validation(
				"User.Delete.UsernameMissing",
				"The username field is required.");
		}

		public static class Get
		{
			public static Error UsernameMissing => Error.Validation(
				"User.Get.UsernameMissing",
				"The username field is required.");
		}
	}
}
