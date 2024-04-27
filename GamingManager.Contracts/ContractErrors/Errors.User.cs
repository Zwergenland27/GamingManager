using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public static class User
	{
		public static class Register
		{
			public static Error UsernameMissing => Error.Validation(
				"User.Register.Username.Missing",
				"The username field is required.");

			public static Error EmailMissing => Error.Validation(
				"User.Register.Email.Missing",
				"The email field is required.");

			public static Error PasswordMissing => Error.Validation(
				"User.Register.Password.Missing",
				"The password field is required.");
		}

		public static class Login
		{
			public static Error IdentifierMissing => Error.Validation(
				"User.Login.Identifier.Missing",
				"The username or email field is required.");
			public static Error PasswordMissing => Error.Validation(
				"User.Login.Password.Missing",
				"The password field is required.");
		}

		public static class RequestEmailVerification
		{
			public static Error UsernameMissing => Error.Validation(
				"User.RequestEmailVerification.Username.Missing",
				"The username field is required.");
		}

		public static class VerifyEmail
		{
			public static Error UsernameMissing => Error.Validation(
				"User.VerifyEmail.Username.Missing",
				"The username field is required.");

			public static Error TokenMissing => Error.Validation(
				"User.VerifyEmail.Token.Missing",
				"The token field is required.");
		}

		public static class RequestPasswordReset
		{
			public static Error EmailMissing => Error.Validation(
				"User.RequestPasswordReset.Email.Missing",
				"The email field is required.");
		}

		public static class EditPersonalData
		{
			public static Error CurrentUsernameMissing => Error.Validation(
				"User.EditPersonalData.CurrentUsername.Missing",
				"The username field is required.");
		}

		public static class ChangePassword
		{
			public static Error UsernameMissing => Error.Validation(
				"User.ChangePassword.Username.Missing",
				"The username field is required.");

			public static Error PasswordMissing => Error.Validation(
				"User.ChangePassword.Password.Missing",
				"The password field is required.");
		}

		public static class Delete
		{
			public static Error UsernameMissing => Error.Validation(
				"User.Delete.Username.Missing",
				"The username field is required.");
		}

		public static class Get
		{
			public static Error UsernameMissing => Error.Validation(
				"User.Get.Username.Missing",
				"The username field is required.");
		}
	}
}
