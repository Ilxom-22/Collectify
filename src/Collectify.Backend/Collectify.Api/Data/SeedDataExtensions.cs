using Bogus;
using Collectify.Application.Identity.Services;
using Collectify.Domain.Entities;
using Collectify.Domain.Enums;
using Collectify.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Collectify.Api.Data;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var passwordHasherService = serviceProvider.GetRequiredService<IPasswordHasherService>();

        if (!await appDbContext.Users.AnyAsync())
            await SeedUsersAsync(appDbContext, passwordHasherService);
    }

    private static async ValueTask SeedUsersAsync(this AppDbContext appDbContext, IPasswordHasherService passwordHasherService)
    {
        var usersFaker = new Faker<User>()
            .RuleFor(user => user.Id, Guid.NewGuid)
            .RuleFor(user => user.UserName, data => data.Internet.UserName())
            .RuleFor(user => user.EmailAddress, data => data.Internet.Email())
            .RuleFor(user => user.Role, data => data.PickRandom<Role>())
            .RuleFor(user => user.Status, data => data.PickRandom<AccountStatus>())
            .RuleFor(user => user.PasswordHash, data => passwordHasherService.HashPassword(data.Internet.Password()));
        
        await appDbContext.Users.AddRangeAsync(usersFaker.Generate(100));
        await appDbContext.SaveChangesAsync();
    }
}