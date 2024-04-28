using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class AccountReadModelConfiguration : IEntityTypeConfiguration<AccountReadModel>
{
	public void Configure(EntityTypeBuilder<AccountReadModel> builder)
	{
		builder.HasKey(account => account.Id);

		builder.HasOne(account => account.Game)
			.WithMany(game => game.Accounts)
			.HasForeignKey(account => account.GameId);

		builder.HasOne(account => account.User)
			.WithMany(user => user.Accounts)
			.HasForeignKey(account => account.UserId);
	}
}
