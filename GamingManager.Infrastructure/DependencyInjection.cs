using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Accounts;
using GamingManager.Application.Features.Games;
using GamingManager.Application.Features.GameServers;
using GamingManager.Application.Features.GameServerTickets;
using GamingManager.Application.Features.Projects;
using GamingManager.Application.Features.Servers;
using GamingManager.Application.Features.Users;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Games;
using GamingManager.Domain.GameServers;
using GamingManager.Domain.GameServerTickets;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Servers;
using GamingManager.Domain.Users;
using GamingManager.Infrastructure.Authentification;
using GamingManager.Infrastructure.Interceptors;
using GamingManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GamingManager.Infrastructure;

public static class DependencyInjection 
{
	public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfigurationManager configuration)
	{
		services.AddInterceptors();
		services.AddGamingManagerDomainContext(configuration);
		services.AddGamingManagerReadContext(configuration);
		services.AddRepositories();
		services.AddJwtAuthentification(configuration);
		return services;
	}

	private static void AddInterceptors(this IServiceCollection services)
	{
		services.AddScoped<PublishDomainEventsInterceptor>();
	}

	private static void AddGamingManagerDomainContext(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("GameServerDb");
		services.AddDbContext<GamingManagerDomainContext>((sp, options) =>
		{
			var interceptor = sp.GetService<PublishDomainEventsInterceptor>()!;

			options.UseNpgsql(connectionString, builder =>
			{
				builder.MigrationsAssembly(typeof(GamingManagerDomainContext).Assembly.FullName);
			});
			options.AddInterceptors(interceptor);
		});
	}

	private static void AddGamingManagerReadContext(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("GameServerDb");
		services.AddDbContext<GamingManagerReadContext>(options =>
		{
			options.UseNpgsql(connectionString, builder =>
			{
				builder.MigrationsAssembly(typeof(GamingManagerReadContext).Assembly.FullName);
				builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
			});
			options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});
	}

	private static void AddRepositories(this IServiceCollection services)
	{
		services
			.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
			.AddTransient<IAccountRepository, AccountRepository>()
			.AddTransient<IAccountDtoRepository, AccountDtoRepository>()
			.AddTransient<IGameRepository, GameRepository>()
			.AddTransient<IGameDtoRepository, GameDtoRepository>()
			.AddTransient<IGameServerRepository, GameServerRepository>()
			.AddTransient<IGameServerDtoRepository, GameServerDtoRepository>()
			.AddTransient<IGameServerTicketRepository, GameServerTicketRepository>()
			.AddTransient<IGameServerTicketDtoRepository, GameServerTicketDtoRepository>()
			.AddTransient<IProjectRepository, ProjectRepository>()
			.AddTransient<IProjectDtoRepository, ProjectDtoRepository>()
			.AddTransient<IServerRepository, ServerRepository>()
			.AddTransient<IServerDtoRepository, ServerDtoRepository>()
			.AddTransient<IUserRepository, UserRepository>()
			.AddTransient<IUserDtoRepository, UserDtoRepository>();
	}

	private static void AddJwtAuthentification(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtSettings = new JwtSettings();
		configuration.Bind(JwtSettings.SectionName, jwtSettings);

		services.AddSingleton(Options.Create(jwtSettings));
		services.AddSingleton<ITokenGenerator, TokenGenerator>();

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
						ValidateIssuer = true,
						ValidIssuer = jwtSettings.Issuer,
						ValidateAudience = true,
						ValidAudience = jwtSettings.Audience,
					};
				});
			}
}
