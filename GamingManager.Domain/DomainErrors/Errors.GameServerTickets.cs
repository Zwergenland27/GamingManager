using CleanDomainValidation.Domain;

namespace GamingManager.Domain.DomainErrors;

public static partial class Errors
{
	public static class GameServerTickets
	{
		/// <summary>
		/// A ticket with this id does not exist
		/// </summary>
		public static Error IdNotFound => Error.NotFound(
			"GameServerTickets.IdNotFound",
			"A ticket with this id does not exist");

		/// <summary>
		/// This ticket has already been closed
		/// </summary>
		public static Error AlreadyClosed => Error.Conflict(
			"GameServerTickets.AlreadyClosed",
			"This ticket has already been closed.");

		/// <summary>
		/// You are not allowed to perform this action
		/// </summary>
		public static Error Forbidden => Error.Forbidden(
			"GameServerTickets.Forbidden",
			"You are not allowed to perform this action.");

		/// <summary>
		/// Errors that occur when working with <see cref="GameServerRequestId"/>
		/// </summary>
		public static class Id
		{
			/// <summary>
			/// The provided id is not a valid guid
			/// </summary>
			public static Error Invalid => Error.Validation(
				"GameServerTickets.Id.Invalid",
				$"The provided id is not a valid guid");
		}
	}
}
