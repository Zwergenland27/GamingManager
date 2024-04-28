namespace GamingManager.Infrastructure.ReadModels;

public class BanReadModel
{
	public Guid Id { get; set; }

	public Guid ProjectId { get; set; }

	public Guid ParticipantId { get; set; }

	public ParticipantReadModel Participant { get; set; }

	public string Reason { get; set; }

	public DateTime BannedAtUtc { get; set; }

	public TimeSpan Duration { get; set; }
}
