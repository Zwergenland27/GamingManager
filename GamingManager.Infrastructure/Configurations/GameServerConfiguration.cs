using GamingManager.Domain.GameServers;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Servers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations;

public class GameServerConfiguration : IEntityTypeConfiguration<GameServer>
{
	public void Configure(EntityTypeBuilder<GameServer> builder)
	{
		ConfigureGameServersTable(builder);
	}

private static void ConfigureGameServersTable(EntityTypeBuilder<GameServer> builder)
	{
		builder.ToTable("GameServers");

		builder.HasKey(gameServer => gameServer.Id);

		builder.HasIndex(gameServer => gameServer.ServerName)
			.IsUnique();

		builder.Property(gameServer => gameServer.Id)
			.HasConversion(
				id => id.Value,
				value => new GameServerId(value));

		builder.Property(gameServer => gameServer.HostedOn)
			.HasConversion(
				hostedOn => hostedOn == null ? default : hostedOn.Value,
				value => value == default ? null : new ServerId(value));

		builder.Property(gameServer => gameServer.Project)
			.HasConversion(
				projectId => projectId.Value,
				value => new ProjectId(value));

		builder.Property(GameServer => GameServer.ServerName)
			.HasConversion(
				servername => servername.Value,
				value => new GameServerName(value));

		builder.Property(gameServer => gameServer.Status);

		builder.Property(gameServer => gameServer.ShutdownDelay)
			.HasConversion(
				shutdownDelay => shutdownDelay.Minutes,
				value => new GameServerAutoShutdownDelay(value));

		builder.Property(gameServer => gameServer.ShutdownAt)
			.HasConversion(
			shutdownAt => shutdownAt == null ? default : shutdownAt.Value,
			value => value == default ? null : new GameServerShutdownAtUtc(value));

		builder.Property(gameServer => gameServer.Address);

		builder.Property(gameServer => gameServer.Maintenance);

		builder.Property(gameServer => gameServer.Unstartable);
	}
}
