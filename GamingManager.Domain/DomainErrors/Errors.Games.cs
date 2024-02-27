using CleanDomainValidation.Domain;
using GamingManager.Domain.Games;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Domain.DomainErrors;

/// <summary>
/// List of all errors
/// </summary>
public static partial class Errors
{
	/// <summary>
	/// Errors that occur when working with <see cref="Game"/> aggregate
	/// </summary>
	public static class Games
	{
		/// <summary>
		/// A game with this id does not exist
		/// </summary>
		public static Error IdNotFound => Error.NotFound(
			"Game.IdNotFound",
			"A game with this id does not exist");

		/// <summary>
		/// A game with this name does not exist
		/// </summary>
		public static Error NameNotFound => Error.NotFound(
			"Game.NameNotFound",
			"A game with this name does not exist");

		/// <summary>
		/// A game with this name already exists in the database
		/// </summary>
		public static Error DuplicateName => Error.Conflict(
			"Game.DuplicateName",
			"A game with ´this name already exist");

		/// <summary>
		/// Errors that occur when working with <see cref="PlayerId"/>
		/// </summary>
		public static class Id
		{
			/// <summary>
			/// The provided id is not a valid guid
			/// </summary>
			public static Error Invalid => Error.Validation(
				"Game.Id.Invalid",
				$"The provided id is not a valid guid");
		}
	}
}
