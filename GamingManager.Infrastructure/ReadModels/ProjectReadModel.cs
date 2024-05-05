namespace GamingManager.Infrastructure.ReadModels;

public class ProjectReadModel
{
	public Guid Id { get; set; }

	public Guid GameId { get; set; }

	public GameReadModel Game { get; set; }

	public Guid? ServerId { get; set; }

	public GameServerReadModel? Server { get; set; }

	public string Name { get; set; }

	public DateTime Start { get; set; }

	public DateTime? End { get; set; }

	public IReadOnlyCollection<GameServerTicketReadModel> Tickets { get; set; }

	public IReadOnlyCollection<ParticipantReadModel> Participants { get; set; }

	public IReadOnlyCollection<MemberReadModel> Members { get; set; }

	public bool Public { get; set; }
}