using CleanDomainValidation.Domain;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Accounts.ValueObjects;

namespace GamingManager.Domain.DomainErrors;

/// <summary>
/// List of all errors
/// </summary>
public static partial class Errors
{
	/// <summary>
	/// Errors that occur when working with <see cref="Account"/>
	/// </summary>
	public static class Accounts
	{
		/// <summary>
		/// The account with this id could not be found
		/// </summary>
		public static Error IdNotFound => Error.NotFound(
			"Account.IdNotFound",
			"An account with this id does not exist");

		/// <summary>
		/// The account with this name could not be found
		/// </summary>
		public static Error NameNotFound => Error.NotFound(
			"Account.NameNotFound",
			"An account with this name does not exist");

		/// <summary>
		/// The account with this uuid could not be found
		/// </summary>
		public static Error UuidNotFound => Error.NotFound(
			"Account.UuidNotFound",
			"An account with this uuid does not exist");

		/// <summary>
		/// A account with this name already exists in the database
		/// </summary>
		public static Error DuplicateName => Error.Conflict(
			"Account.DuplicateName",
			"An account with this name already exist");

		/// <summary>
		/// A account with this uuid already exists in the database
		/// </summary>
		public static Error DuplicateUuid => Error.Conflict(
			"Account.DuplicateUuid",
			"An account with this uuid already exist");

		/// <summary>
		/// Errors that occur when working with <see cref="User"/>
		/// </summary>
		public static class User
		{
			/// <summary>
			/// A user should be assigned to the player which has already an assigned user
			/// </summary>
			public static Error AlreadyAssigned => Error.Conflict(
				"Account.User.AlreadyAssigned",
				"A user is already assigned to this account");
		}

		/// <summary>
		/// Errors that occur when working with <see cref="Uuid"/>
		/// </summary>
		public static class Uuid
		{
			/// <summary>
			/// A user should be assigned to the player which has already an assigned user
			/// </summary>
			public static Error AlreadyAssigned => Error.Conflict(
				"Account.Uuid.AlreadyAssigned",
				"A uuid is already assigned to this account");
		}

		/// <summary>
		/// Errors that occur when working with <see cref="AccountId"/>
		/// </summary>
		public static class Id
		{
			/// <summary>
			/// The provided id is not a valid guid
			/// </summary>
			public static Error Invalid => Error.Validation(
				"Player.Id.Invalid",
			$"The provided id is not a valid guid");
		}
	}
}
