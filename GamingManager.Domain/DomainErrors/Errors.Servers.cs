using CleanDomainValidation.Domain;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Domain.DomainErrors;

/// <summary>
/// List of all errors
/// </summary>
public static partial class Errors
{
	/// <summary>
	/// Errors that occur when working with <see cref="Server"/> aggregate
	/// </summary>
	public static class Servers
	{
		/// <summary>
		/// The server with this id could not be found
		/// </summary>
		public static Error IdNotFound => Error.NotFound(
			"Server.IdNotFound",
			"A server with this id does not exist");

		/// <summary>
		/// The server with this alias could not be found
		/// </summary>
		public static Error AliasNotFound => Error.NotFound(
			"Server.AliasNotFound",
			"A server with this alias does not exist");

		/// <summary>
		/// A server with this alias already exists in the database
		/// </summary>
		public static Error DuplicateAlias => Error.Conflict(
			"Server.DuplicateAlias",
			"A server with this alias already exist");

		/// <summary>
		/// The provided ping is before the last ping
		/// </summary>
		public static Error PingBeforeLast => Error.Conflict(
			"Server.PingBeforeLast",
			"The provided ping is before the last ping");

		/// <summary>
		/// No maintenance was started
		/// </summary>
		public static Error NoMaintenance => Error.Conflict(
			"Server.NoMaintenance",
			"No maintenance was started");

		/// <summary>
		/// The server cannot be started because it is currently undergoing maintenance
		/// </summary>
		public static Error NotStartable => Error.Conflict(
			"Server.NotStartable",
			"The server cannot be started because it is currently undergoing maintenance");

		/// <summary>
		/// The server cannot be started because it is already online
		/// </summary>
		public static Error AlreadyOnline => Error.Conflict(
			"Server.AlreadyOnline",
			"The server cannot be started because it is already online");

		/// <summary>
		/// The server has already been started
		/// </summary>
		public static Error AlreadyStarting => Error.Conflict(
			"Server.AlreadyStarting",
			"The server has already been started");

		/// <summary>
		/// The shutdown cannot be scheduled in the past
		/// </summary>
		public static Error ShutdownInPast => Error.Conflict(
			"Server.ShutdownInPast",
			"The shutdown cannot be scheduled in the past");

		/// <summary>
		/// No shutdown has been scheduled
		/// </summary>
		public static Error NoShutdownScheduled => Error.Conflict(
			"Server.NoShutdownScheduled",
			"No shutdown has been scheduled");

		/// <summary>
		/// The shutdown cannot be executed before its scheduled time
		/// </summary>
		public static Error ShutdownTooEarly => Error.Conflict(
			"Server.ShutdownTooEarly",
			"The shutdown cannot be executed before its scheduled time");

		/// <summary>
		/// Cannot shutdown a server that is currently starting
		/// </summary>
		public static Error CannotShutdownStarting => Error.Conflict(
			"Server.CannotShutdownStarting",
			"Cannot shutdown a server that is currently starting");

		/// <summary>
		/// Cannot shutdown a server that is currently offline
		/// </summary>
		public static Error CannotShutdownOffline => Error.Conflict(
			"Server.CannotStopOffline",
			"Cannot shutdown a server that is currently offline");

		/// <summary>
		/// Errors that occur when working with <see cref="ServerId"/>
		/// </summary>
		public static class Id
		{
			/// <summary>
			/// The provided id is not a valid guid
			/// </summary>
			public static Error Invalid => Error.Validation(
				"Server.Id.Invalid",
				"The provided id is not a valid guid");
		}
	}
}
