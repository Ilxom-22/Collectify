using System.Text;
using Collectify.Application.Identity.Models.Settings;
using Collectify.Domain;
using Collectify.Persistence.DataContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
    
    private static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        
        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection(nameof(JwtSettings)));
        
        var jwtSettings = new JwtSettings();
        builder.Configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

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