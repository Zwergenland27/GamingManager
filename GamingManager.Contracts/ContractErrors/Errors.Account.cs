using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
    public static class Account
    {
        public static class AssignUser
        {
            /// <summary>
            /// The game name field is missing
            /// </summary>
            public static Error GameNameMissing => Error.Validation(
                "Account.AssignUser.GameNameMissing",
                "The gameName field is required.");

            /// <summary>
            /// The account name field is missing
            /// </summary>
            public static Error AccountNameMissing => Error.Validation(
                "Account.AssignUser.AccountNameMissing",
                "The accountName field is required.");

            /// <summary>
            /// The username field is missing
            /// </summary>
            public static Error UsernameMissing => Error.Validation(
                "Account.AssignUser.UsernameMissing",
                "The username field is required.");
        }

        public static class AssignUuid
        {
            /// <summary>
            /// The game name field is missing
            /// </summary>
            public static Error GameNameMissing => Error.Validation(
                "Account.CreateFromUser.GameNameMissing",
                "The gameName field is required.");

            /// <summary>
            /// The account name field is missing
            /// </summary>
            public static Error AccountNameMissing => Error.Validation(
                "Account.CreateFromUser.AccountNameMissing",
                "The accountName field is required.");

            /// <summary>
            /// The username field is missing
            /// </summary>
            public static Error UuidMissing => Error.Validation(
                "Account.CreateFromUser.UuidMissing",
                "The uuid field is required.");
        }

        public static class CreateFromUser
        {
            /// <summary>
            /// The game name field is missing
            /// </summary>
            public static Error GameNameMissing => Error.Validation(
                "Account.CreateFromUser.GameNameMissing",
                "The gameName field is required.");

            /// <summary>
            /// The account name field is missing
            /// </summary>
            public static Error AccountNameMissing => Error.Validation(
                "Account.CreateFromUser.AccountNameMissing",
                "The accountName field is required.");

            /// <summary>
            /// The username field is missing
            /// </summary>
            public static Error UsernameMissing => Error.Validation(
                "Account.CreateFromUser.UsernameMissing",
                "The username field is required.");
        }

        public static class ReAssignUser
        {
            /// <summary>
            /// The game name field is missing
            /// </summary>
            public static Error GameNameMissing => Error.Validation(
                "Account.ReAssignFromUser.GameNameMissing",
                "The gameName field is required.");

            /// <summary>
            /// The account name field is missing
            /// </summary>
            public static Error AccountNameMissing => Error.Validation(
                "Account.ReAssignFromUser.AccountNameMissing",
                "The accountName field is required.");

            /// <summary>
            /// The username field is missing
            /// </summary>
            public static Error UsernameMissing => Error.Validation(
                "Account.ReAssignFromUser.UsernameMissing",
                "The username field is required.");
        }

        public static class Get
        {
			/// <summary>
			/// The game name field is missing
			/// </summary>
			public static Error GameNameMissing => Error.Validation(
				"Account.Get.GameNameMissing",
				"The gameName field is required.");

			/// <summary>
			/// The account name field is missing
			/// </summary>
			public static Error AccountNameMissing => Error.Validation(
				"Account.Get.AccountNameMissing",
				"The accountName field is required.");
		}

        public static class GetAll
        {
			/// <summary>
            /// The game name field is missing
            /// </summary>
            public static Error GameNameMissing => Error.Validation(
                "Account.GetAll.GameNameMissing",
                "The gameName field is required.");
        }
    }
}
