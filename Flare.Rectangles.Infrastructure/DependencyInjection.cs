using System.Diagnostics.CodeAnalysis;
using Flare.Rectangles.Application.Common.Interfaces;
using Flare.Rectangles.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flare.Rectangles.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services
			.AddScoped<IGridService, GridService>();

		return services;
	}
}
