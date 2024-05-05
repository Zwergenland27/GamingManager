using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Games;
using GamingManager.Domain.GameServerRequests;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Users;
using GamingManager.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure;

public class GamingManagerDomainContext(DbContextOptions<GamingManagerDomainContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : DbContext(options)
{
	private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor = publishDomainEventsInterceptor;

	public DbSet<Account> Accounts { get; set; }

	public DbSet<Game> Games { get; set; }

	public DbSet<GameServer> GameServers { get; set; }

	public DbSet<GameServerTicket> GameServerTickets { get; set; }

	public DbSet<Project> Projects { get; set; }

	public DbSet<Server> Servers { get; set; }

	public DbSet<User> Users { get; set; }


	private static bool DomainConfigurationsFilter(Type type) => type.FullName?.Contains("Configurations.Domain") ?? false;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.Ignore<List<IDomainEvent>>()
			.ApplyConfigurationsFromAssembly(typeof(GamingManagerDomainContext).Assembly,
			DomainConfigurationsFilter);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
		base.OnConfiguring(optionsBuilder);
	}
}
