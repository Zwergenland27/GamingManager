using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GamingManager.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
	{
		services.AddMediator();

		return services;
	}

	private static IServiceCollection AddMediator(this IServiceCollection services)
	{
		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
		});
		return services;
	}
}
