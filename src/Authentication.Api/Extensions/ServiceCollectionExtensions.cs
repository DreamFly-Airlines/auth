using Authentication.Domain.ValueObjects;

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
}