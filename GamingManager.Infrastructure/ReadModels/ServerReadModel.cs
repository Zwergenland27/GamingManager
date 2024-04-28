using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Infrastructure.ReadModels;

public class ServerReadModel
{
	public Guid Id { get; set; }

	public string Hostname { get; set; }

	public string Address { get; set; }

	public string Mac { get; set; }

	public ServerStatus Status { get; set; }

	public uint ShutdownDelay { get; set; }

	public DateTime? ShutdownAt { get; set; }

	public DateTime? LastHeartbeatAt { get; set; }

	public bool Maintenance { get; set; }

	public bool PossiblyUnstartable { get; set; }

	public IReadOnlyCollection<GameServerReadModel> Hosts { get; set; }

}
