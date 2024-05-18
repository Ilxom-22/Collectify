using Collectify.Application.Identity.Models.Settings;
using Collectify.Application.Identity.Services;
using Collectify.Domain.Entities;
using Collectify.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace Collectify.Infrastructure.Identity.Services;

public class AccessTokenService(
    IAccessTokenGeneratorService accessTokenGeneratorService,
    IAccessTokenRepository accessTokenRepository,
    IOptions<JwtSettings> jwtSettings) : IAccessTokenService
{
    public async ValueTask<AccessToken> GetByUserIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default!)
    {
        var foundToken = await accessTokenRepository.GetByUserIdAsync(userId, asNoTracking, cancellationToken) 
                         ?? throw new ArgumentException($"Access token with user id {userId} does not exist!");

        return foundToken;
    }
    
    public async ValueTask<AccessToken> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await DeleteByUserIdIfExists(user.Id, cancellationToken);
        
        var accessToken = new AccessToken
        {
            UserId = user.Id,
            Token = accessTokenGeneratorService.GetToken(user),
            ExpiryTime = DateTimeOffset.UtcNow.AddMinutes(jwtSettings.Value.ExpirationTimeInMinutes)
        };

        return await accessTokenRepository.CreateAsync(accessToken, cancellationToken: cancellationToken);
    }

    public async ValueTask<AccessToken> DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var foundToken = await GetByUserIdAsync(userId, cancellationToken: cancellationToken);
        return await accessTokenRepository.DeleteAsync(foundToken, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Removes previous access token of the user if any exists
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    public async ValueTask DeleteByUserIdIfExists(Guid userId, CancellationToken cancellationToken = default)
    {
        var foundToken = await accessTokenRepository.GetByUserIdAsync(userId, cancellationToken: cancellationToken);

        if (foundToken is not null)
            await accessTokenRepository.DeleteAsync(foundToken, cancellationToken: cancellationToken);
    }
}