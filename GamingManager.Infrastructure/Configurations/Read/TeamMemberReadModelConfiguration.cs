using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

internal class TeamMemberReadModelConfiguration : IEntityTypeConfiguration<TeamMemberReadModel>
{
	public void Configure(EntityTypeBuilder<TeamMemberReadModel> builder)
	{
		builder.HasKey(teamMember => teamMember.Id);

		builder.HasOne(teamMember => teamMember.Project)
			.WithMany(project => project.TeamMembers)
			.HasForeignKey(teamMember => teamMember.ProjectId);
	}
}
