using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class TeamMemberReadModelConfiguration : IEntityTypeConfiguration<MemberReadModel>
{
	public void Configure(EntityTypeBuilder<MemberReadModel> builder)
	{
		builder.HasKey(teamMember => teamMember.Id);

		builder.HasOne(teamMember => teamMember.Project)
			.WithMany(project => project.Members)
			.HasForeignKey(teamMember => teamMember.ProjectId);
	}
}
