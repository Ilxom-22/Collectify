using Collectify.Domain;
using Collectify.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Collectify.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
    
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }
    
    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => options.AddPolicy("AllowSpecificOrigin",
            policy => policy
                .WithOrigins(builder.Configuration["ApiClientSettings:WebClientAddress"]!)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var dbConnectionString = builder.Environment.IsDevelopment()
            ? builder.Configuration.GetConnectionString(ConnectionStringConstants.LocalDbConnectionString)
            : Environment.GetEnvironmentVariable(ConnectionStringConstants.RemoteDbConnectionString);
        
        builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(dbConnectionString));
        
        return builder;
    }
    
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
    
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
    
    private static WebApplication UseCors(this WebApplication app)
    {
        app.UseCors("AllowSpecificOrigin");

        return app;
    }
}