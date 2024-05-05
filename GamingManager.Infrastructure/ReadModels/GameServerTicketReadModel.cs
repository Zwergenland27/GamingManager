using GamingManager.Domain.GameServerTickets.ValueObjects;

namespace GamingManager.Infrastructure.ReadModels;

public class GameServerTicketReadModel
{
	public Guid Id { get; set; }

	public Guid ProjectId { get; set; }

	public ProjectReadModel Project { get; set; }

	public Guid ApplicantId { get; set; }

	public UserReadModel Applicant { get; set; }

	public Guid IssuerId { get; set; }

	public UserReadModel Issuer { get; set; }

	public string Title { get; set; }

	public string Details { get; set; }

	public string? Annotation { get; set; }

	public TicketStatus Status { get; set; }
}
