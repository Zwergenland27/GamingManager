using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.GameServers.ValueObjects;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Projects.Entities;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingManager.Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
	public void Configure(EntityTypeBuilder<Project> builder)
	{
		ConfigureProjectsTable(builder);
		ConfigureParticipantsTable(builder);
		ConfigureTeamMembersTable(builder);
	}

	private static void ConfigureProjectsTable(EntityTypeBuilder<Project> builder)
	{
		builder.ToTable("Projects");

		builder.HasKey(project => project.Id);

		builder.HasIndex(project => project.Name);

		builder.Property(project => project.Id)
			.HasConversion(
				projectId => projectId.Value,
				value => new ProjectId(value));

		builder.Property(project => project.Game)
			.HasConversion(
				gameId => gameId.Value,
				value => new GameId(value));

		builder.Property(project => project.Server)
			.HasConversion(
				serverId => serverId == null ? default : serverId.Value,
				value => value == default ? null : new GameServerId(value));

		builder.Property(project => project.Name)
			.HasConversion(
				projectName => projectName.Value,
				value => new ProjectName(value));

		builder.Property(project => project.Start)
			.HasConversion(
				start => start.Value,
				value => new ProjectStartsAtUtc(value));

		builder.Property(project => project.End)
			.HasConversion(
				end => end == null ? default : end.Value,
				value => value == default ? null : new ProjectEndsAtUtc(value));

		builder.Property(project => project.Ended);
	}

	private static void ConfigureParticipantsTable(EntityTypeBuilder<Project> builder)
	{
		builder.OwnsMany(project => project.Participants, participantBuilder =>
		{
			participantBuilder.ToTable("Participants");

			participantBuilder.WithOwner().HasForeignKey(participant => participant.Project);

			participantBuilder.HasKey(participant => participant.Id);

			participantBuilder.HasIndex(participant => new { participant.Id, participant.Project })
				.IsUnique();

			participantBuilder.Property(participant => participant.Id)
				.HasConversion(
					participantId => participantId.Value,
					value => new ParticipantId(value));

			participantBuilder.Property(participant => participant.Project)
				.HasConversion(
					projectId => projectId.Value,
					value => new ProjectId(value));

			participantBuilder.Property(participant => participant.Account)
				.HasConversion(
					accountId => accountId.Value,
					value => new AccountId(value));

			participantBuilder.Property(participant => participant.Since)
				.HasConversion(
					since => since.Value,
					value => new ParticipatesSinceUtc(value));

			participantBuilder.Property(participant => participant.Online);

			participantBuilder.Property(participant => participant.PlayTime)
				.UsePropertyAccessMode(PropertyAccessMode.Field);

			participantBuilder.Ignore(participant => participant.IsCurrentlyBanned);

			ConfigureBansTable(participantBuilder);
			ConfigureSessionsTable(participantBuilder);
		});

		builder.Metadata.FindNavigation(nameof(Project.Participants))!
			.SetPropertyAccessMode(PropertyAccessMode.Field);
	}

	private static void ConfigureBansTable(OwnedNavigationBuilder<Project, Participant> builder)
	{
		builder.OwnsMany(participant => participant.Bans, banBuilder =>
		{
			banBuilder.ToTable("Bans");

			banBuilder.WithOwner().HasForeignKey(ban => ban.Participant);

			banBuilder.HasKey(ban => ban.Id);

			banBuilder.HasIndex(ban => new {ban.Id, ban.Participant})
				.IsUnique();

			banBuilder.Property(ban => ban.Id)
				.HasConversion(
					banId => banId.Value,
					value => new BanId(value));

			banBuilder.Property(ban => ban.Participant)
				.HasConversion(
					participantId => participantId.Value,
					value => new ParticipantId(value));

			banBuilder.Property(ban => ban.Reason)
				.HasConversion(
					reason => reason.Value,
					value => Reason.Create(value).Value);

			banBuilder.Property(ban => ban.BannedAtUtc);

			banBuilder.Property(ban => ban.Duration);
		});
		builder.Navigation(participant => participant.Bans)
			.UsePropertyAccessMode(PropertyAccessMode.Field);
	}

	private static void ConfigureSessionsTable(OwnedNavigationBuilder<Project, Participant> builder)
	{
		builder.OwnsMany(participant => participant.Sessions, sessionBuilder =>
		{
			sessionBuilder.ToTable("Sessions");

			sessionBuilder.WithOwner().HasForeignKey(session => session.Participant);

			sessionBuilder.HasKey(session => session.Id);

			sessionBuilder.HasIndex(session => new { session.Id, session.Participant })
				.IsUnique();

			sessionBuilder.Property(session => session.Id)
				.HasConversion(
					sessionId => sessionId.Value,
					value => new SessionId(value));

			sessionBuilder.Property(session => session.Participant)
				.HasConversion(
					participantId => participantId.Value,
					value => new ParticipantId(value));

			sessionBuilder.Property(sessionBuilder => sessionBuilder.Start)
				.HasConversion(
					start => start.Value,
					value => new SessionStartsAtUtc(value));

			sessionBuilder.OwnsOne(session => session.End);

			sessionBuilder.Ignore(session => session.Duration);
		});

		builder.Navigation(participant => participant.Sessions)
			.UsePropertyAccessMode(PropertyAccessMode.Field);
	}

	private static void ConfigureTeamMembersTable(EntityTypeBuilder<Project> builder)
	{
		builder.OwnsMany(project => project.Team, teamMemberBuilder =>
		{
			teamMemberBuilder.ToTable("TeamMembers");

			teamMemberBuilder.WithOwner().HasForeignKey(teamMember => teamMember.Project);

			teamMemberBuilder.HasKey(teamMember => teamMember.Id);

			teamMemberBuilder.HasIndex(teamMember => new { teamMember.Id, teamMember.Project })
				.IsUnique();

			teamMemberBuilder.Property(teamMember => teamMember.Id)
				.HasConversion(
					teamMemberId => teamMemberId.Value,
					value => new TeamMemberId(value));

			teamMemberBuilder.Property(teamMember => teamMember.Project)
				.HasConversion(
					projectId => projectId.Value,
					value => new ProjectId(value));

			teamMemberBuilder.Property(teamMember => teamMember.User)
				.HasConversion(
					userId => userId.Value,
					value => new UserId(value));

			teamMemberBuilder.Property(teamMember => teamMember.Role);

			teamMemberBuilder.Property(teamMember => teamMember.Since)
				.HasConversion(
					since => since.Value,
					value => new TeamMemberSinceUtc(value));
		});

		builder.Metadata.FindNavigation(nameof(Project.Team))!
			.SetPropertyAccessMode(PropertyAccessMode.Field);
	}
}
