using GamingManager.Domain.GameServerRequests;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.GameServerTickets.ValueObjects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations.Domain;

public class GameServerTicketConfiguration : IEntityTypeConfiguration<GameServerTicket>
{
	public void Configure(EntityTypeBuilder<GameServerTicket> builder)
	{
		builder.ToTable("GameServerTickets");

		builder.HasKey(ticket => ticket.Id);

		builder.Property(ticket => ticket.Id)
			.HasConversion(
				id => id.Value,
				value => new GameServerTicketId(value));

		builder.Property(ticket => ticket.ProjectId)
			.HasConversion(
				id => id.Value,
				value => new ProjectId(value));

		builder.Property(ticket => ticket.ApplicantId)
			.HasConversion(
				id => id.Value,
				value => new UserId(value));

		builder.Property(ticket => ticket.Title)
			.HasConversion(
				title => title.Value,
				value => new TicketTitle(value));

		builder.Property(ticket => ticket.Details)
			.HasConversion(
				details => details.Value,
				value => new TicketDetails(value));

		builder.Property(ticket => ticket.Annotation)
			.HasConversion(
				annotation => annotation == null ? null : annotation.Value,
				value => value == null ? null :  new TicketDetails(value));

		builder.Property(ticket => ticket.Status);
	}
}
