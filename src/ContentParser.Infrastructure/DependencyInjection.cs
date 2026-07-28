using ContentParser.Application.Interfaces;
using ContentParser.Application.Services;
using ContentParser.Domain.Interfaces;
using ContentParser.Infrastructure.Services;
using ContentParser.Infrastructure.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace ContentParser.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IContentParserStrategy, CsvContentParserStrategy>();
        services.AddTransient<IContentParserStrategy, JsonContentParserStrategy>();
        
        services.AddScoped<IContentParserResolver, ContentParserResolver>();
        services.AddScoped<IBase64Decoder, Base64Decoder>();

        return services;
    }
}