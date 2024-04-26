using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public static class Project
	{
		public static class AddToTeam
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.AddToTeam.ProjectId.Missing",
				"The projectId field is required.");

			public static Error UsernameMissing => Error.Validation(
				"Project.AddToTeam.Username.Missing",
				"The username field is required.");

			public static Error RoleMissing => Error.Validation(
				"Project.AddToTeam.Role.Missing",
				"The role field is required.");

			public static Error RoleInvalid => Error.Validation(
				"Project.AddToTeam.Role.Invalid",
				"The role field is not a valid team role.");
		}

		public static class AllowAccount
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.AllowAccount.ProjectId.Missing",
				"The projectId field is required.");

			public static Error AccountIdMissing => Error.Validation(
				"Project.AllowAccount.AccountId.Missing",
				"The accountId field is required.");
		}

		public static class BanAccount
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.BanAccount.ProjectId.Missing",
				"The projectId field is required.");

			public static Error AccountIdMissing => Error.Validation(
				"Project.BanAccount.AccountId.Missing",
				"The accountId field is required.");

			public static Error ReasonMissing => Error.Validation(
				"Project.BanAccount.Reason.Missing",
				"The reason field is required.");

			public static Error DurationMissing => Error.Validation(
				"Project.BanAccount.Duration.Missing",
				"The duration field is required.");
		}

		public static class Create
		{
			public static Error GameNameMissing => Error.Validation(
				"Project.Create.GameName.Missing",
				"The gameName field is required.");

			public static Error ProjectNameMissing => Error.Validation(
				"Project.Create.ProjectName.Missing",
				"The projectName field is required.");

			public static Error StartsAtUtcMissing => Error.Validation(
				"Project.Create.StartsAtUtc.Missing",
				"The startsAtUtc field is required.");

			public static Error UsernameMissing => Error.Validation(
				"Project.Create.Username.Missing",
				"The username field is required.");
		}

		public static class Finish
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.Delete.Finish.Missing",
				"The projectId field is required.");
		}

		public static class Join
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.Join.ProjectId.Missing",
				"The projectId field is required.");

			public static Error UuidMissing => Error.Validation(
				"Project.Join.Uuid.Missing",
				"The uuid field is required.");

			public static Error JoinTimeUtcMissing => Error.Validation(
				"Project.Join.JoinTimeUtc.Missing",
				"The joinTimeUtc field is required.");
		}

		public static class Leave
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.Leave.ProjectId.Missing",
				"The projectId field is required.");

			public static Error UuidMissing => Error.Validation(
				"Project.Leave.Uuid.Missing",
				"The uuid field is required.");

			public static Error LeaveTimeUtcMissing => Error.Validation(
				"Project.Leave.LeaveTimeUtc.Missing",
				"The leaveTimeUtc field is required.");
		}

        public static class Pardon
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.Pardon.ProjectId.Missing",
				"The projectId field is required.");

			public static Error AccountIdMissing => Error.Validation(
				"Project.Pardon.AccountId.Missing",
				"The accountId field is required.");
		}

		public static class RemoveFromTeam
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.RemoveFromTeam.ProjectId.Missing",
				"The projectId field is required.");

			public static Error UsernameMissing => Error.Validation(
				"Project.RemoveFromTeam.Username.Missing",
				"The username field is required.");
		}

		public static class Reschedule
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.Reschedule.ProjectId.Missing",
				"The projectId field is required.");

			public static Error PlannedStartUtcMissing => Error.Validation(
				"Project.Reschedule.NewStartAtUtc.Missing",
				"The plannedStartUtc field is required.");
		}

		public static class SetPlannedEnd
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.SetPlannedEnd.ProjectId.Missing",
				"The projectId field is required.");

			public static Error PlannedEndAtUtcMissing => Error.Validation(
				"Project.SetPlannedEnd.PlannedEndUtc.Missing",
				"The plannedEndUtc field is required.");
		}

		public static class Get
		{
			public static Error ProjectIdMissing => Error.Validation(
				"Project.Get.ProjectId.Missing",
				"The projectId field is required.");
		}
    }
}
