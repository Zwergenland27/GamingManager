using CleanDomainValidation.Domain;
using GamingManager.Domain.Users.ValueObjects;


namespace GamingManager.Domain.DomainErrors;

/// <summary>
/// List of all errors
/// </summary>
public static partial class Errors
{
	/// <summary>
	/// Errors that occur when working with <see cref="Users"/> aggregate
	/// </summary>
	public static class Users
	{
		/// <summary>
		/// The user with this id could not be found
		/// </summary>
		public static Error IdNotFound => Error.NotFound(
			"User.IdNotFound",
			"A user with this id does not exist");

		/// <summary>
		/// The username / email or password is incorrect.
		/// </summary>
		public static Error InvalidLogin => Error.Conflict(
			"User.InvalidLogin",
			"The username / email or password is incorrect.");

		public static Error InvalidEmailVerification => Error.Conflict(
			"User.InvalidEmailVerification",
			"The email verification token is invalid or has expired");

		/// <summary>
		/// The user with this username could not be found
		/// </summary>
		public static Error UsernameNotFound => Error.NotFound(
			"User.UsernameNotFound",
			"A user with this username does not exist");

		/// <summary>
		/// A user with this username already exists in the database
		/// </summary>
		public static Error DuplicateUsername => Error.Conflict(
			"User.DuplicateUsername",
			"A user with this username already exist");

		/// <summary>
		/// A user with this email already exists
		/// </summary>
		public static Error DuplicateEmail => Error.Conflict(
			"User.DuplicateEmail",
			"A user with this email already exists");

		/// <summary>
		/// No changes were made to the user data
		/// </summary>
		public static Error NoChanges => Error.Conflict(
			"User.NoChanges",
			"No changes were made to the user data");

		/// <summary>
		/// Errors that occur when working with <see cref="UserId"/>
		/// </summary>
		public static class Id
		{
			/// <summary>
			/// The provided id is not a valid guid
			/// </summary>
			public static Error Invalid => Error.Validation(
				"User.Id.Invalid",
				$"The provided id is not a valid guid");
		}

		public static class Email
		{
			public static Error Invalid => Error.Validation(
				"User.Email.Invalid",
				$"The provided email is not a valid email");
		}
	}
}
