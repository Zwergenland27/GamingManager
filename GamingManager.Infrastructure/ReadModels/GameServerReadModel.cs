using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Infrastructure.ReadModels;

public class GameServerReadModel
{
	public Guid Id { get; set; }

	public Guid? HostedOnId { get; set; }

	public ServerReadModel HostedOn { get; set; }

	public Guid ProjectId { get; set; }

	public ProjectReadModel Project { get; set; }

	public string ServerName { get; set; }

	public GameServerStatus Status { get; set; }

	public uint ShutdownDelay { get; set; }

	public DateTime? ShutdownAt { get; set; }

	public string? Address { get; set; }

	public bool Maintenance { get; set; }

	public bool Unstartable { get; set; }
}
