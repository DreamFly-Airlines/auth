using System.Reflection;
using Authentication.Domain.ValueObjects;
using Shared.Abstractions.Commands;
using Shared.Abstractions.Queries;

namespace Authentication.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddJwtOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptionsSection = configuration.GetSection("JwtOptions");
        var key = jwtOptionsSection.GetValue<string>("Key");
        var expiresInSeconds = jwtOptionsSection.GetValue<int>("ExpiresInSeconds");
        var jwtOptions = new JwtOptions(key!, TimeSpan.FromSeconds(expiresInSeconds));
        services.AddSingleton(jwtOptions);
    }

    public static void AddQueryHandlers(
        this IServiceCollection services,
        Assembly assembly)
    {
        var handlerInterfaceType = typeof(IQueryHandler<,>);
        services.FindImplementationsAndRegister(handlerInterfaceType, assembly);
    }

    public static void AddCommandHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlerInterfaceType = typeof(ICommandHandler<>);
        services.FindImplementationsAndRegister(handlerInterfaceType, assembly);
    }
    
    private static void FindImplementationsAndRegister(this IServiceCollection services, Type interfaceType, Assembly assembly)
    {
        var types = assembly
            .GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .Select(t => new
            {
                Implementation = t,
                Interfaces = t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
            });

        foreach (var type in types)
            foreach (var @interface in type.Interfaces)
                services.AddScoped(@interface, type.Implementation);
    }
}