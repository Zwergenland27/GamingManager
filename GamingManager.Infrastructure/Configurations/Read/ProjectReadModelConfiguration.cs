using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class ProjectReadModelConfiguration : IEntityTypeConfiguration<ProjectReadModel>
{
	public void Configure(EntityTypeBuilder<ProjectReadModel> builder)
	{
		builder.HasKey(project => project.Id);

		builder.HasOne(project => project.Game)
			.WithMany(game => game.Projects)
			.HasForeignKey(project => project.GameId);

		builder.HasOne(project => project.Server)
			.WithOne(gameServer => gameServer.Project)
			.HasForeignKey<ProjectReadModel>(project => project.ServerId);
	}
}
