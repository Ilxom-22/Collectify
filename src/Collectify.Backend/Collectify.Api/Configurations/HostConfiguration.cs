namespace Collectify.Api.Configurations;

public static partial class HostConfiguration
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder
            .AddCors()
            .AddDevTools()
            .AddExposers()
            .AddPersistence();
        
        return builder;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app
            .UseCors()
            .UseDevTools()
            .UseExposers();
        
        return app;
    }
}