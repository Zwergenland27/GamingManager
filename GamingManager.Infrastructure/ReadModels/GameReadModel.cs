namespace GamingManager.Infrastructure.ReadModels;

public class GameReadModel
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	public bool VerificationRequired { get; set; }

	public IReadOnlyCollection<AccountReadModel> Accounts { get; set; }

	public IReadOnlyCollection<ProjectReadModel> Projects { get; set; }
}
