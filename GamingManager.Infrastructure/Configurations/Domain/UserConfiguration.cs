using GamingManager.Domain.Users;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Domain;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    private static void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value));

        builder.Property(user => user.Firstname)
            .HasConversion(
                firstname => firstname == null ? null : firstname.Value,
                value => value == null ? null : new Firstname(value));

        builder.Property(user => user.Lastname)
            .HasConversion(
                lastname => lastname == null ? null : lastname.Value,
                value => value == null ? null : new Lastname(value));

        builder.Property(user => user.Username)
            .HasConversion(
                username => username.Value,
                value => new Username(value));

        builder.Property(user => user.Role);

        builder.Property(user => user.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value);

        builder.Property("_passwordHash");

        builder.Property("_passwordSalt");

        builder.OwnsOne<EmailVerificationToken>("_emailVerificationToken");

        builder.OwnsOne<RefreshToken>("_refreshToken");
    }
}
