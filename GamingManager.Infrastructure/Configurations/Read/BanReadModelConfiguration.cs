using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class BanReadModelConfiguration : IEntityTypeConfiguration<BanReadModel>
{
	public void Configure(EntityTypeBuilder<BanReadModel> builder)
	{
		builder.HasKey(ban => ban.Id);

		builder.HasOne(ban => ban.Participant)
			.WithMany(participant => participant.Bans)
			.HasForeignKey(ban => ban.ParticipantId);
	}
}
