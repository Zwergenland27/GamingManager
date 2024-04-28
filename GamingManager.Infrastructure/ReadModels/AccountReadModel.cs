using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamingManager.Infrastructure.ReadModels;

public class AccountReadModel
{
	public Guid Id { get; set; }

	public Guid? UserId { get; set; }

	public UserReadModel? User { get; set; }

	public Guid? GameId { get; set; }

	public GameReadModel Game { get; set; }

	public string? Uuid { get; set; }

	[NotMapped]
	public bool Verified => UserId is not null && Uuid is not null;

	public string Name { get; set; }

	public IReadOnlyCollection<ParticipantReadModel> Participants { get; set; }
}
