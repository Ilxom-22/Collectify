using Collectify.Domain.Entities;

namespace Collectify.Application.Identity.Services;

public interface IAccessTokenService
{
    ValueTask<AccessToken> GetByUserIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default!);
    
    ValueTask<AccessToken> CreateAsync(
        User user,
        CancellationToken cancellationToken = default);

    ValueTask<AccessToken> DeleteByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    ValueTask DeleteByUserIdIfExists(
        Guid userId,
        CancellationToken cancellationToken = default);
}