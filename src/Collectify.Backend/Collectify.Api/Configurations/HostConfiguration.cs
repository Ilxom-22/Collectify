namespace Collectify.Api.Configurations;

public static partial class HostConfiguration
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder
            .AddCors()
            .AddDevTools()
            .AddExposers()
            .AddPersistence()
            .AddJwtAuthentication()
            .AddIdentityInfrastructure()
            .AddValidators()
            .AddMappers();
        
        return builder;
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseCors()
            .UseDevTools()
            .UseExposers();

        await app.MigrateDatabaseSchemaAsync();
        
        return app;
    }
}