using GamingManager.Domain.Servers;
using GamingManager.Domain.Servers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations;

public class ServerConfiguration : IEntityTypeConfiguration<Server>
{
	public void Configure(EntityTypeBuilder<Server> builder)
	{
		ConfigureServersTable(builder);
	}

	private static void ConfigureServersTable(EntityTypeBuilder<Server> builder)
	{
		builder.ToTable("Servers");

		builder.HasKey(server => server.Id);

		builder.Property(server => server.Id)
			.HasConversion(
				id => id.Value,
				value => new ServerId(value));

		builder.Property(server => server.Hostname)
			.HasConversion(
				hostname => hostname.Value,
				value => Hostname.Create(value).Value);

		builder.Property(server => server.Address)
			.HasConversion(
				address => address.ToString(),
				value => new Uri(value));

		builder.Property(server => server.Mac)
			.HasConversion(
				mac => mac.Value,
				value => Mac.Create(value).Value);

		builder.Property(server => server.Status);

		builder.Property(server => server.ShutdownDelay)
			.HasConversion(
				shutdownDelay => shutdownDelay.Minutes,
				value => new ServerAutoShutdownDelay(value));

		builder.Property(server => server.ShutdownAt)
			.HasConversion(
				shutdownAt => shutdownAt == null ? default : shutdownAt.Value,
				value => value == default ? null : new ServerShutdownAtUtc(value));

		builder.Property(server => server.LastHeartbeatAt)
			.HasConversion(
				lastHeartbeatAt => lastHeartbeatAt == null ? default : lastHeartbeatAt.Value,
				value => value == default ? null : new HeartbeatReceivedAtUtc(value));

		builder.Property(server => server.Maintenance);

		builder.Property(server => server.PossiblyUnstartable);
	}
}
