using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class SessionReadModelConfiguration : IEntityTypeConfiguration<SessionReadModel>
{
	public void Configure(EntityTypeBuilder<SessionReadModel> builder)
	{
		builder.HasKey(session => session.Id);

		builder.HasOne(session => session.Participant)
			.WithMany(participant => participant.Sessions)
			.HasForeignKey(session => session.ParticipantId);
	}
}
