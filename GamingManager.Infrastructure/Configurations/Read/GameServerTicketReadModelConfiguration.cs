using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Read;

public class GameServerTicketReadModelConfiguration : IEntityTypeConfiguration<GameServerTicketReadModel>
{
	public void Configure(EntityTypeBuilder<GameServerTicketReadModel> builder)
	{
		builder.ToTable("GameServerTickets");

		builder.HasKey(ticket => ticket.Id);

		builder.HasOne(ticket => ticket.Project)
			.WithMany(project => project.Tickets)
			.HasForeignKey(ticket => ticket.ProjectId);

		builder.HasOne(ticket => ticket.Applicant)
			.WithMany(user => user.CreatedTickets)
			.HasForeignKey(ticket => ticket.ApplicantId);

		builder.HasOne(ticket => ticket.Issuer)
			.WithMany(user => user.IssuedTickets)
			.HasForeignKey(ticket => ticket.IssuerId);
	}
}
