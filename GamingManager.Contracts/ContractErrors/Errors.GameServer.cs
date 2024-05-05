using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public static class GameServer
	{
		public static class CancelShutdown
		{
			/// <summary>
			/// The name field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.CancelShutdown.Name.Missing",
				"The name field is required.");

			/// <summary>
			/// The user name field is required
			/// </summary>
			public static Error UsernameMissing => Error.Validation(
				"GameServer.CancelShutdown.Username.Missing",
				"The username field is required.");
        }

		public static class ChangeShutdownDelay
		{
			/// <summary>
			/// The name field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.ChangeShutdownDelay.NameMissing",
				"The name field is required.");
			 																	
			/// <summary>
			/// The delay field is required
			/// </summary>
			public static Error DelayMissing => Error.Validation(
				"GameServer.ChangeShutdownDelay.DelayMissing",
				"The delay field is required.");
		}

		public static class Crashed
		{
			/// <summary>
			/// The name field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Crashed.NameMissing",
				"The name field is required.");

			/// <summary>
			/// The crashed at utc field is required
			/// </summary>
			public static Error CrashedAtUtcMissing => Error.Validation(
				"GameServer.Crashed.CrashedAtUtcMissing",
				"The crashed At Utc field is required.");
		}

		public static class Create
		{
			/// <summary>
			/// The name field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Create.NameMissing",
				"The name field is required.");
			
			/// <summary>
			/// The project name field is required
			/// </summary>
			public static Error ProjectNameMissing => Error.Validation(
				"GameServer.Create.ProjectNameMissing",
				"The projectName field is required.");
			
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error ShutdownDelayMissing => Error.Validation(
				"GameServer.Create.ShutdownDelayMissing",
				"The shutdownDelay field is required.");
		}

		public static class Delete
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Delete.NameMissing",
				"The name field is required.");
		}

		public static class ShuttedDown
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.ShuttedDown.NameMissing",
				"The name field is required.");
		}

		public static class Start
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Start.NameMissing",
				"The name field is required.");
		}

		public static class Started
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Started.NameMissing",
				"The name field is required.");
		}

		public static class UseServer
		{
			/// <summary>
			/// The name field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Create.NameMissing",
				"The name field is required.");

			/// <summary>
			/// The hostname field is required
			/// </summary>
			public static Error HostnameMissing => Error.Validation(
				"GameServer.UseServer.HostnameMissing",
				"The hostname field is required.");
		}

		public static class Get
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Get.NameMissing",
				"The name field is required.");
		}
	}
}
