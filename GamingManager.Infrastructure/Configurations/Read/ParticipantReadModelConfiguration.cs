using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class ParticipantReadModelConfiguration : IEntityTypeConfiguration<ParticipantReadModel>
{
	public void Configure(EntityTypeBuilder<ParticipantReadModel> builder)
	{
		builder.HasKey(participant => participant.Id);

		builder.HasOne(participant => participant.Account)
			.WithMany(account => account.Participants)
			.HasForeignKey(participant => participant.AccountId);

		builder.HasOne(participant => participant.Project)
			.WithMany(project => project.Participants)
			.HasForeignKey(participant => participant.ProjectId);
	}
}
