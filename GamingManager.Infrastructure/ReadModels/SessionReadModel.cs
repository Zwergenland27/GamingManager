namespace GamingManager.Infrastructure.ReadModels;

public class SessionReadModel
{
	public Guid Id { get; set; }

	public Guid ProjectId { get; set; }

	public Guid ParticipantId { get; set; }

	public ParticipantReadModel Participant { get; set; }

	public DateTime Start { get; set; }

	public DateTime? End { get; set; }

	public bool Irregular { get; set; }
}
