using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class GameServerReadModelConfiguration : IEntityTypeConfiguration<GameServerReadModel>
{
	public void Configure(EntityTypeBuilder<GameServerReadModel> builder)
	{
		builder.HasKey(gameServer => gameServer.Id);

		builder.HasOne(gameServer => gameServer.HostedOn)
			.WithMany(server => server.Hosts)
			.HasForeignKey(gameServer => gameServer.HostedOnId);
	}
}
