using CleanDomainValidation.Domain;

namespace GamingManager.Contracts.ContractErrors;

public static partial class Errors
{
	public static class Game
	{
		public static class Create
		{
			public static Error NameMissing => Error.Validation(
				"Games.Create.NameMissing",
				"The name field is missing");
		}
	}
}
