using GamingManager.Domain.Accounts;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
	public void Configure(EntityTypeBuilder<Account> builder)
	{
		ConfigureAccountsTable(builder);
	}

	private static void ConfigureAccountsTable(EntityTypeBuilder<Account> builder)
	{
		builder.ToTable("Accounts");

		builder.HasKey(account => account.Id);

		builder.HasIndex(account => new { account.Game, account.Uuid });
		builder.HasIndex(account => new { account.Game, account.Name });

		builder.Property(account => account.Id)
			.HasConversion(
				id => id.Value,
				value => new AccountId(value));

		builder.Property(account => account.User)
			.HasConversion(
				value => value == null ? default : value.Value,
				userId => userId == default ? null : new UserId(userId));

		builder.Property(account => account.Game)
			.HasConversion(
				gameId => gameId.Value,
				value => new GameId(value));

		builder.Property(account => account.Uuid)
			.HasConversion(
				uuid => uuid == null ? null : uuid.Value,
				value => value == null ? null : new Uuid(value));

		builder.Property(account => account.Name)
			.HasConversion(
				name => name.Value,
				value => new AccountName(value));

		builder.Ignore(account => account.IsConfirmed);
	}
}
