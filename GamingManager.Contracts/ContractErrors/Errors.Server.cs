using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public class Server
	{
		public static class Uri
		{
			public static Error Invalid => Error.Validation(
				"Uri.Invalid",
				"The address is not a valid URI.");
		}

		public static class ChangeServerAddress
		{
			public static Error HostnameMissing => Error.Validation(
				"Server.ChangeServerAddress.Hostname.Missing",
				"The hostname field is required.");

			public static Error AddressMissing => Error.Validation(
				"Server.ChangeServerAddress.Address.Missing",
				"The address field is required.");
		}

		public static class ChangeHostname
		{
			public static Error CurrentHostnameMissing => Error.Validation(
				"Server.ChangeHostname.CurrentHostname.Missing",
				"The current hostname field is required.");

			public static Error NewHostnameMissing => Error.Validation(
				"Server.ChangeHostname.NewHostname.Missing",
				"The new hostname field is required.");
		}

		public static class CreateServer
		{
			public static Error HostnameMissing => Error.Validation(
				"Server.CreateServer.Hostname.Missing",
				"The hostname field is required.");

			public static Error AddressMissing => Error.Validation(
				"Server.CreateServer.Address.Missing",
				"The address field is required.");

			public static Error MacMissing => Error.Validation(
				"Server.CreateServer.Mac.Missing",
				"The mac field is required.");

			public static Error ShutdownDelayMissing => Error.Validation(
				"Server.CreateServer.ShutdownDelay.Missing",
				"The shutdown delay field is required.");
		}

        public static class  Delete
		{
			public static Error HostnameMissing => Error.Validation(
				"Server.Delete.Hostname.Missing",
				"The hostname field is required.");
		}

		public static class ReceiveHeartbeat
		{
			public static Error HostnameMissing => Error.Validation(
				"Server.ReceiveHeartbeat.Hostname.Missing",
				"The hostname field is required.");
		}

		public static class Start
		{
			public static Error HostnameMissing => Error.Validation(
				"Server.Start.Hostname.Missing",
				"The hostname field is required.");
		}

        public static class  Get
        {
			public static Error HostnameMissing => Error.Validation(
				"Server.Get.Hostname.Missing",
				"The hostname field is required.");
		}
    }
}
