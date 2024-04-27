using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Accounts;
using GamingManager.Application.Features.Games;
using GamingManager.Application.Features.GameServers;
using GamingManager.Application.Features.Projects;
using GamingManager.Application.Features.Servers;
using GamingManager.Application.Features.Users;
using GamingManager.Domain.Accounts;
using GamingManager.Domain.Games;
using GamingManager.Domain.GameServers;
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
		services.AddGamingManagerContext(configuration);
		services.AddRepositories();
		services.AddJwtAuthentification(configuration);
		return services;
	}

	private static void AddInterceptors(this IServiceCollection services)
	{
		services.AddScoped<PublishDomainEventsInterceptor>();
	}

	private static void AddGamingManagerContext(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("GameServerDb");
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
			.AddTransient<IAccountDtoRepository, AccountDtoRepository>()
			.AddTransient<IGameRepository, GameRepository>()
			.AddTransient<IGameDtoRepository, GameDtoRepository>()
			.AddTransient<IGameServerRepository, GameServerRepository>()
			.AddTransient<IGameServerDtoRepository, GameServerDtoRepository>()
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
		services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

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
