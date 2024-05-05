using GamingManager.Domain.Games;
using GamingManager.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Domain;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        ConfigureGamesTable(builder);
    }

    private static void ConfigureGamesTable(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");

        builder.HasKey(game => game.Id);

        builder.HasIndex(game => game.Name)
            .IsUnique();

        builder.Property(game => game.Id)
            .HasConversion(
                gameId => gameId.Value,
                value => new GameId(value));

        builder.Property(game => game.Name)
            .HasConversion(
                gameName => gameName.Value,
                value => new GameName(value));

        builder.Property(game => game.VerificationRequired);
    }
}
