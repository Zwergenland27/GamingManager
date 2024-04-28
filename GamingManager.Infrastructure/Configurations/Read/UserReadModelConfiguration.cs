using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class UserReadModelConfiguration : IEntityTypeConfiguration<UserReadModel>
{
	public void Configure(EntityTypeBuilder<UserReadModel> builder)
	{
		builder.HasKey(user => user.Id);
	}
}
