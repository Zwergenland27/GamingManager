using CleanDomainValidation.Domain;

namespace GamingManager.Domain.DomainErrors;

/// <summary>
/// List of all errors
/// </summary>
public static partial class Errors
{
	public static class GameServers
	{
		/// <summary>
		/// The operation can only be done when the game server is in maintenance mode
		/// </summary>
		public static Error MaintenanceNeeded => Error.Conflict(
			"GameServer.MaintenanceNeeded",
			"The operation can only be done when the game server is in maintenance mode");

		/// <summary>
		/// The provided ping is before the last ping
		/// </summary>
		public static Error PingBeforeLast => Error.Conflict(
			"GameServer.PingBeforeLast",
			"The provided ping is before the last ping");

		/// <summary>
		/// No maintenance was started
		/// </summary>
		public static Error NoMaintenance => Error.Conflict(
			"GameServer.NoMaintenance",
			"No maintenance was started");

		/// <summary>
		/// A shutdown cannot be scheduled in the past
		/// </summary>
		public static Error ShutdownInPast => Error.Conflict(
			"GameServer.ShutdownInPast",
			"A shutdown cannot be scheduled in the past");

		/// <summary>
		/// No shutdown has been scheduled
		/// </summary>
		public static Error NoShutdownScheduled => Error.Conflict(
			"GameServer.NoShutdownScheduled",
			"No shutdown has been scheduled");

		/// <summary>
		/// The game server is starting, so cannot be shut down
		/// </summary>
		public static Error CannotShutdownStarting => Error.Conflict(
			"GameServer.CannotShutdownStarting",
			"The game server is starting, so cannot be shut down");

		/// <summary>
		/// The game server is offline, so cannot be shut down
		/// </summary>
		public static Error CannotShutdownOffline => Error.Conflict(
			"GameServer.CannotShutdownOffline",
			"The game server is offline, so cannot be shut down");

		/// <summary>
		/// The game server is offline, so cannot crash
		/// </summary>
		public static Error CannotCrash => Error.Conflict(
			"GameServer.CannotCrash",
			"The game server is offline, so cannot crash");

		/// <summary>
		/// The game server cannot be shutted down before the shutdown has been scheduled
		/// </summary>
		public static Error ShutdownTooEarly => Error.Conflict(
			"GameServer.ShutdownTooEarly",
			"The game server cannot be shutted down before the shutdown has been scheduled");

		/// <summary>
		/// The game server is currently maintained
		/// </summary>
		public static Error NotStartable => Error.Conflict(
			"GameServer.NotStartable",
			"The game server is currently maintained");

		/// <summary>
		/// The game server is already online
		/// </summary>
		public static Error AlreadyOnline => Error.Conflict(
			"GameServer.AlreadyOnline",
			"The game server is already online");

		/// <summary>
		///The game server is already starting 
		/// </summary>
		public static Error AlreadyStarting => Error.Conflict(
			"GameServer.AlreadyStarting",
			"The game server is already starting");

		/// <summary>
		/// The game server cannot be started in the future
		/// </summary>
		public static Error StartedInFuture => Error.Conflict(
			"GameServer.StartedInFuture",
			"The game server cannot be started in the future");

		/// <summary>
		/// Errors that occur when working with <see cref="GameServerId"/>
		/// </summary>
		public static class Id
		{
			/// <summary>
			/// The provided id is not a valid guid
			/// </summary>
			public static Error Invalid(string id) => Error.Validation(
				"GameServer.Id.Invalid",
				$"The provided game server id (\"{id}\") is not a valid guid");
		}
	}
}
