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
				"GameServer.CancelShutdown.NameMissing",
				"The Name field is required.");
        }

		public static class ChangeShutdownDelay
		{
			/// <summary>
			/// The name field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.ChangeShutdownDelay.NameMissing",
				"The Name field is required.");
			 																	
			/// <summary>
			/// The delay field is required
			/// </summary>
			public static Error DelayMissing => Error.Validation(
				"GameServer.ChangeShutdownDelay.DelayMissing",
				"The Delay field is required.");
		}

		public static class Crashed
		{
			/// <summary>
			/// The name field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Crashed.NameMissing",
				"The Name field is required.");

			/// <summary>
			/// The crashed at utc field is required
			/// </summary>
			public static Error CrashedAtUtcMissing => Error.Validation(
				"GameServer.Crashed.CrashedAtUtcMissing",
				"The Crashed At Utc field is required.");
		}

		public static class Create
		{
			/// <summary>
			/// The name field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Create.NameMissing",
				"The Name field is required.");
			
			/// <summary>
			/// The project name field is required
			/// </summary>
			public static Error ProjectNameMissing => Error.Validation(
				"GameServer.Create.ProjectNameMissing",
				"The Project Name field is required.");
			
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error ShutdownDelayMissing => Error.Validation(
				"GameServer.Create.ShutdownDelayMissing",
				"The Shutdown Delay field is required.");
		}

		public static class Delete
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Delete.NameMissing",
				"The Name field is required.");
		}

		public static class ShuttedDown
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.ShuttedDown.NameMissing",
				"The Name field is required.");
		}

		public static class Start
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Start.NameMissing",
				"The Name field is required.");
		}

		public static class Started
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Started.NameMissing",
				"The Name field is required.");
		}

		public static class UseServer
		{
			/// <summary>
			/// The name field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Create.NameMissing",
				"The Name field is required.");

			/// <summary>
			/// The hostname field is required
			/// </summary>
			public static Error HostnameMissing => Error.Validation(
				"GameServer.UseServer.HostnameMissing",
				"The Hostname field is required.");
		}

		public static class Get
		{
			/// <summary>
			/// The shutdown delay field is required
			/// </summary>
			public static Error NameMissing => Error.Validation(
				"GameServer.Get.NameMissing",
				"The Name field is required.");
		}
	}
}
