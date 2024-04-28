using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class GameReadModelConfiguration : IEntityTypeConfiguration<GameReadModel>
{
	public void Configure(EntityTypeBuilder<GameReadModel> builder)
	{
		builder.HasKey(game => game.Id);
	}
}
