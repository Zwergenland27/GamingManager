namespace GamingManager.Infrastructure.ReadModels;

public class ParticipantReadModel
{
	public Guid Id { get; set; }

	public Guid ProjectId { get; set; }

	public ProjectReadModel Project { get; set; }

	public Guid AccountId { get; set; }

	public AccountReadModel Account { get; set; }

	public DateTime Since { get; set; }

	public bool Online { get; set; }

	public TimeSpan PlayTime { get; set; }

	public IReadOnlyCollection<BanReadModel> Bans { get; set; }

	public IReadOnlyCollection<SessionReadModel> Sessions { get; set; }
}
