using GamingManager.Infrastructure.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure;

public class GamingManagerReadContext(DbContextOptions<GamingManagerReadContext> options) : DbContext(options)
{
	public DbSet<AccountReadModel> Accounts { get; set; }

	public DbSet<GameReadModel> Games { get; set; }

	public DbSet<GameServerReadModel> GameServers { get; set; }

	public DbSet<ProjectReadModel> Projects { get; set; }

	public DbSet<TeamMemberReadModel> TeamMembers { get; set; }

	public DbSet<ParticipantReadModel> Participants { get; set; }

	public DbSet<BanReadModel> Bans { get; set; }

	public DbSet<SessionReadModel> Sessions { get; set; }

	public DbSet<ServerReadModel> Servers { get; set; }

	public DbSet<UserReadModel> Users { get; set; }


	private static bool ReadConfigurationsFilter(Type type) => type.FullName?.Contains("Configurations.Read") ?? false;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.ApplyConfigurationsFromAssembly(typeof(GamingManagerDomainContext).Assembly,
			ReadConfigurationsFilter);
	}
}
