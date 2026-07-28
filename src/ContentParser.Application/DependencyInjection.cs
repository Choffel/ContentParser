using ContentParser.Application.Interfaces;
using ContentParser.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContentParser.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IContentParseService, ContentParseService>();
        return services;
    }
}