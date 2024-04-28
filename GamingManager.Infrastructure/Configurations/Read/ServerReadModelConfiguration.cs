using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class ServerReadModelConfiguration : IEntityTypeConfiguration<ServerReadModel>
{
	public void Configure(EntityTypeBuilder<ServerReadModel> builder)
	{
		builder.HasKey(server => server.Id);
	}
}
