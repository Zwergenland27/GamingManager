using GamingManager.Application.Abstractions;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Games;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Users;
using GamingManager.Infrastructure.Interceptors;
using GamingManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamingManager.Infrastructure;

public static class DependencyInjection 
{
	public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddInterceptors();
		services.AddGamingManagerContext(configuration);
		services.AddRepositories();
		return services;
	}

	private static void AddInterceptors(this IServiceCollection services)
	{
		services.AddScoped<PublishDomainEventsInterceptor>();
	}

	private static void AddGamingManagerContext(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("TelemetryDbConnection");
		services.AddDbContext<GamingManagerContext>((sp, options) =>
		{
			var interceptor = sp.GetService<PublishDomainEventsInterceptor>()!;

			options.UseNpgsql(connectionString, builder =>
			{
				builder.MigrationsAssembly(typeof(GamingManagerContext).Assembly.FullName);
			});
			options.AddInterceptors(interceptor);
		});
	}

	private static void AddRepositories(this IServiceCollection services)
	{
		services
			.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
			.AddTransient<IAccountRepository, AccountRepository>()
			.AddTransient<IGameRepository, GameRepository>()
			.AddTransient<IGameServerRepository, GameServerRepository>()
			.AddTransient<IProjectRepository, ProjectRepository>()
			.AddTransient<IServerRepository, ServerRepository>()
			.AddTransient<IUserRepository, UserRepository>();
	}
}
